using Ai_Panel.Application.DTOs.AiConfig;
using MediatR;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.AiConfig.Request.Command;

public class UpsertAiConfigRequest : IRequest<ServiceMessage>
{
    public UpsertAiConfigDto UpsertAiConfig { get; set; }
}