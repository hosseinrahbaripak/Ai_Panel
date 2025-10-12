using MediatR;
using PersianAssistant.Models;

namespace Live_Book.Application.Features.AiChat.Request.Queries;

public class GetUserAiChatHistoryRequest : IRequest<ServiceMessage>
{
    public int UserId { get; set; }
    public int? PartId { get; set; }
    public int QuestionId { get; set; }
    public int AiConfigId { get; set; }
}