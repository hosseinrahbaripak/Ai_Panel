using Ai_Panel.Application.DTOs.AiChat;

namespace Ai_Panel.Application.DTOs.AiConfig;

public class TestAiConfigDto
{
    public int? PartId { get; set; }
    public int? BookId { get; set; }
    public int? QuestionId { get; set; }
    public int AdminId { get; set; }
    public int AiPlatformId { get; set; }
    public int AiId { get; set; }
    public string AiStr { get; set; }
    public int AiModelId { get; set; }
    public string AiModelStr { get; set; }
    public int MaxTokens { get; set; }
    public string Message { get; set; }
	public string FirstMessage { get; set; }
	public string? BookContent { get; set; }
    public string Prompt { get; set; }
    public int N { get; set; } = 1;
    public float Temperature { get; set; }
    public float TopP { get; set; }
    public float PresencePenalty { get; set; }
    public float FrequencyPenalty { get; set; }
    public string StopStr { get; set; }
    public string[] Stop => !string.IsNullOrEmpty(StopStr) ? StopStr.Split("،") : [];
    public double SummarizationCost { get; set; }
    public double RequestCost { get; set; }
    public double EmbeddingCost { get; set; }
    public string? AiResponse { get; set; }
	public DateTime? TestDateTimeBegging { get; set; }
	public List<ChatHistoryDto> chat_history { get; set; }
}