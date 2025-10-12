using MediatR;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.AiChat.Request.Queries;

public class GetUserAiChatHistoryRequest : IRequest<ServiceMessage>
{
    public int UserId { get; set; }
    public int? PartId { get; set; }
    public int QuestionId { get; set; }
    public int AiConfigId { get; set; }
}