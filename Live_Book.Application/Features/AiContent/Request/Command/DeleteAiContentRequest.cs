using MediatR;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.AiContent.Request.Command;
public class DeleteAiContentRequest : IRequest<ServiceMessage>
{
	public int Id { get; set; }
	public int AiConfigId {  get; set; }
	public bool Bulk { get; set; }
}

