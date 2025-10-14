using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using Ai_Panel.Application.DTOs.AiChat;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Services.Ai
{
    
    public class AiApiClient : IAiApiClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public AiApiClient(HttpClient httpClient , IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiKey = _configuration["AVAL_AI_KEY"];

        }

        public async Task<ServiceMessage> GetChatCompletionAsync(ChatCompletionDto dto)
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
            if (response.StatusCode != HttpStatusCode.OK)
            {
                string ErrorTitle = ErrorHandling(response.StatusCode);
                return new ServiceMessage()
                {
                    ErrorId = -1,
                    ErrorTitle = ErrorTitle,
                    Result = null
                };
            }
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JsonNode.Parse(content);
            string AiResponse = json?["choices"]?[0]?["message"]?["content"]?.ToString();
            return new ServiceMessage()
            {
                ErrorId = 0,
                ErrorTitle = null,
                Result = AiResponse
            };
        }
        private string ErrorHandling(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                    return "api مورد نظر یافت نشد";
                case HttpStatusCode.Unauthorized:
                    return "کلید API شما اشتباه است.";
                case HttpStatusCode.Forbidden:
                    return "شما اجازه دسترسی به این منبع را ندارید.";
                case HttpStatusCode.BadRequest:
                    return "درخواست شما نامعتبر است.";
                case HttpStatusCode.TooManyRequests:
                    return "شما از محدودیت نرخ خود فراتر رفته‌اید.";
                case HttpStatusCode.InternalServerError:
                    return "خطایی در سرور رخ داده است";
                default:
                    return "";
            }


        }
    }
}
