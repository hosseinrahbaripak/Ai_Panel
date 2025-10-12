using Ai_Panel.Application.DTOs.AiChat;
using MediatR;

namespace Ai_Panel.Application.Features.AiChat.Request.Queries;
public class GetUserAiChatLogsRequest : IRequest<AiChatDashboardDto>
{
	public UserAiChatLogsFilter? ModelFilter { get; set; }
}
