using Ai_Panel.Application.DTOs.AiChat;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Services.Ai
{
    public interface IAiApiClient
    {
        Task<ServiceMessage> GetChatCompletionAsync(ChatCompletionDto dto);
    }
}
