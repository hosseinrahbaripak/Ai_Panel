using PersianAssistant.Extensions;

namespace Ai_Panel.Application.DTOs.AiChat;

public class AiChatResponseDto
{
    public int Id { get; set; }
    public string UserMessage { get; set; }
    public string UserName { get; set; }
    public string AiResponse { get; set; }
    public int? BookId { get; set; }
    public int? PartId { get; set; }
    public int? QuestionId { get; set; }
    public DateTime DateTime { get; set; }
    public string DatePersian => DateTime.ToPersianDateTime();
}