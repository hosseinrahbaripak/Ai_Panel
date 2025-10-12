using Live_Book.Application.DTOs.AiConfig;
using MediatR;
using PersianAssistant.Models;

namespace Live_Book.Application.Features.AiConfig.Request.Command;

public class UpsertAiConfigRequest : IRequest<ServiceMessage>
{
    public UpsertAiConfigDto UpsertAiConfig { get; set; }
}