using Live_Book.Application.DTOs.AiConfig;
using MediatR;
using PersianAssistant.Models;

namespace Live_Book.Application.Features.AiConfig.Request.Queries;

public class GetAiConfigRequest : IRequest<UpsertAiConfigDto?>
{
    public int Id { get; set; }
}