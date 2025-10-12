using Live_Book.Application.DTOs.AiChat;

namespace Live_Book.Application.DTOs.TestAiConfig;
public class TestAiConfigFilter : UserAiChatLogsFilter
{
	public int AiModelId { get; set; }
	public int AiId { get; set; }
}
