using Ai_Panel.Application.DTOs.AiChat;

namespace Ai_Panel.Application.DTOs.TestAiConfig;
public class TestAiConfigFilter : UserAiChatLogsFilter
{
	public int AiModelId { get; set; }
	public int AiId { get; set; }
}
