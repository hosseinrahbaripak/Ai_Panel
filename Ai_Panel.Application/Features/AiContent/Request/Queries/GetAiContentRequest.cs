using Ai_Panel.Application.DTOs.AiContent;
using MediatR;

namespace Ai_Panel.Application.Features.AiContent.Request.Queries;

public class GetAiContentRequest : IRequest<AiContentUpsertDto?>
{
    public int Id { get; set; }
    public int AiConfigId { get; set; }
}