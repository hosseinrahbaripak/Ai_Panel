using Live_Book.Application.DTOs.AiChat;
using MediatR;

namespace Live_Book.Application.Features.AiChat.Request.Queries;
public class GetUserAiChatLogsRequest : IRequest<AiChatDashboardDto>
{
	public UserAiChatLogsFilter? ModelFilter { get; set; }
}
