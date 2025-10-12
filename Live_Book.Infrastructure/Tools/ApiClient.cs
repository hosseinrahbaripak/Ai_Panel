using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;
namespace Live_Book.Infrastructure.Tools;
public class ApiClient
{
	private string Url { get; set; }
	private string Method { get; set; }
	public object Body { get; set; }
	public JsonSerializerSettings Settings { get; set; }
	public Dictionary<string, string?> QueryParams { get; set; }
	public Dictionary<string, string?> Headers { get; set; }
	public TimeSpan Timeout { get; set; }
	private HttpClient _client;
	public ApiClient(string url, string method="get")
	{
		Url = url;
		Method = method;
		QueryParams = new Dictionary<string, string?>();
		Headers = new Dictionary<string, string?>();
		_client = new HttpClient();
	}
	public async Task<ApiResult<T>> Send<T>()
	{
		SetHeaders();
		SetQueryParams();
		HttpResponseMessage response;
		switch (Method.ToLower())
		{
			case "post":
				response = await Post(); break;
			default:
				response = await Get(); break;
		}
		var result = new ApiResult<T> { StatusCode = response.StatusCode };
		if (!response.IsSuccessStatusCode)
		{
			result.IsSuccess = false;
			result.ErrorMessage = await response.Content.ReadAsStringAsync();
			return result;
		}
		var data = await Desrialize<T>(response);
		result.Data = data;
		result.IsSuccess = true;
		return result;
	}
	private void SetHeaders()
	{
		if(Headers?.Count > 0)
		{
			foreach (var item in Headers)
			{
				_client.DefaultRequestHeaders.Add(item.Key, item.Value);
			}
		}
	}
	private async Task<T> Desrialize<T>(HttpResponseMessage response)
	{
		var json = await response.Content.ReadAsStringAsync();
		var res = JsonConvert.DeserializeObject<T>(json);
		return res;
	}
	private void SetQueryParams()
	{
		if(QueryParams?.Count > 0)
		{
			var builder = new StringBuilder();
			builder.Append(Url);
			builder.Append('?');
			foreach (var item in QueryParams)
			{
				var key = Uri.EscapeDataString(item.Key); // escape invalid characters of url
				var value = Uri.EscapeDataString(item.Value ?? "");
				builder.Append($"{key}={value}&");
			}
			builder.Length--; // remove last &
			Url = builder.ToString();
		}
	}
	private async Task<HttpResponseMessage> Post()
	{
		Settings ??= new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        var jsonRequestData = JsonConvert.SerializeObject(Body, Settings);
		var requestContent = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
		HttpResponseMessage response = await _client.PostAsync(Url, requestContent);
		return response;
	}
	private async Task<HttpResponseMessage> Get()
	{
		HttpResponseMessage response = await _client.GetAsync(Url);
		return response;
	}
}
public class ApiResult<T>
{
	public bool IsSuccess { get; set; }
	public T? Data { get; set; }
	public string? ErrorMessage { get; set; }
	public HttpStatusCode? StatusCode { get; set; }
}
public class ApiResponse
{
	public int? ErrorId { get; set; }
	public string? ErrorTitle { get; set; }
	public string? Result { get; set; }
}
public class ApiResponse<T>
{
	public int? ErrorId { get; set; }
	public string? ErrorTitle { get; set; }
	public T Result { get; set; }
}