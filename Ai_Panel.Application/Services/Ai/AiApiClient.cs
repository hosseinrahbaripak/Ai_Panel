using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using Ai_Panel.Application.DTOs.AiChat;
using Newtonsoft.Json;

namespace Ai_Panel.Application.Services.Ai
{
    public class AiApiClient : IAiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "";

        public AiApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetChatCompletionAsync(ChatCompletionDto dto)
        {
            var requestBody = new
            {
                model = dto.Model,
                messages = new[]
                {
                    new { role = "system", content = dto.Prompt ?? "You are a helpful assistant." },
                    new { role = "user", content = dto.Message }
                },
                temperature = dto.Temperature,
                top_p = dto.TopP,
                frequency_penalty = dto.FrequencyPenalty,
                presence_penalty = dto.PresencePenalty,
                max_tokens = dto.MaxTokens,
                stop = dto.Stop
            };

            var request = new HttpRequestMessage(HttpMethod.Post, dto.BaseUrl)
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
