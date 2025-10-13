using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace Ai_Panel.Application.Services.Ai
{
    public class AiApiClient : IAiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "";

        private const string BaseUrl = "https://api.avalai.ir/v1/chat/completions";

        public AiApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetChatCompletionAsync(string prompt)
        {
            var requestBody = new
            {
                model = "gpt-4o",
                messages = new[]
                {
                    new { role = "user", content = prompt } ,

                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl)
            {
                Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
            };

            request.Headers.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JsonNode.Parse(content);
            return json?["choices"]?[0]?["message"]?["content"]?.ToString();
        }
    }
}
