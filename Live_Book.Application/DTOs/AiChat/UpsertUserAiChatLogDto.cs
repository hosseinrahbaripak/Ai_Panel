namespace Live_Book.Application.DTOs.AiChat;

public class UpsertUserAiChatLogDto
{
    public int Id { get; set; }
    public string UserMessage { get; set; }
    public bool AiCouldResponse { get; set; }
    public string? AiResponse { get; set; }
    public int BookId { get; set; }
    public int PartId { get; set; }
    public int UserId { get; set; }
    public int? QuestionId { get; set; }
    public int AiConfigId { get; set; }
    public double SummarizationCost { get; set; }
    public double RequestCost { get; set; }
    public double EmbeddingCost { get; set; }
    public DateTime DateTime { get; set; }
    public Domain.User User { get; set; }
}