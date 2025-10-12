using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace Live_Book.Infrastructure.Mhelli;
public static class FileHelper
{
	public static async Task<bool> AddFile(string fileName, IFormFile file, string bucket = "livebook")
	{
		try
		{
			string pdfBase64 = "";
			HttpClient client = new HttpClient();
			await using (var ms = new MemoryStream())
			{
				await file.CopyToAsync(ms);
				var fileBytes = ms.ToArray();
				pdfBase64 = Convert.ToBase64String(fileBytes);
			}
			var pdfFileModel = new FileModel()
			{
				key = fileName,
				File = pdfBase64
			};
			client.DefaultRequestHeaders.Add("AwsBucketName", bucket);
			HttpResponseMessage response = await client.PostAsJsonAsync<FileModel>("https://file.mhelli.com/api/AddFile", pdfFileModel);
			var status = response.StatusCode;
			if (status is HttpStatusCode.OK or HttpStatusCode.Created)
			{
				return true;
			}
			return false;
		}
		catch (Exception e)
		{
			return false;
		}
	}
	public static async Task<string> GetFile(string fileName, string bucket = "livebook")
	{
		HttpClient client = new HttpClient();
		client.DefaultRequestHeaders.Add("AwsBucketName", bucket);
		HttpResponseMessage response = await client.GetAsync($"https://file.mhelli.com/api/GetFile?name={fileName}&timeout=15");
		var res = await response.Content.ReadAsStringAsync();
		return res;
	}
	public static async Task DeleteFile(string fileName, string bucket = "livebook")
	{
		HttpClient client = new HttpClient();
		client.DefaultRequestHeaders.Add("AwsBucketName", bucket);
		HttpResponseMessage response = await client.GetAsync($"https://file.mhelli.com/api/DeleteFile?name={fileName}");
		var res = await response.Content.ReadAsStringAsync();
	}
}
public class FileModel
{
	public string key { get; set; }
	public string? Url { get; set; }
	public string File { get; set; }

}