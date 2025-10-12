using Live_Book.Application.DTOs.AiContent;
using MediatR;

namespace Live_Book.Application.Features.AiContent.Request.Queries;

public class GetAiContentRequest : IRequest<AiContentUpsertDto?>
{
    public int Id { get; set; }
    public int AiConfigId { get; set; }
}