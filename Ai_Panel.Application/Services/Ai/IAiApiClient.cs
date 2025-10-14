using Ai_Panel.Application.DTOs.AiChat;

namespace Ai_Panel.Application.Services.Ai
{
    public interface IAiApiClient
    {
        Task<string?> GetChatCompletionAsync(ChatCompletionDto dto);
    }
}
