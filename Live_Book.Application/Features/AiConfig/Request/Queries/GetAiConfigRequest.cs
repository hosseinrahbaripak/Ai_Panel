using Ai_Panel.Application.DTOs.AiConfig;
using MediatR;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.AiConfig.Request.Queries;

public class GetAiConfigRequest : IRequest<UpsertAiConfigDto?>
{
    public int Id { get; set; }
}