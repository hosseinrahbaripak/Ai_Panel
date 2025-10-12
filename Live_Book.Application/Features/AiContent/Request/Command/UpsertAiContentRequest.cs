using Ai_Panel.Application.DTOs.AiContent;
using MediatR;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.AiContent.Request.Command;

public class UpsertAiContentRequest : IRequest<ServiceMessage>
{
    public AiContentUpsertDto Model { get; set; }
}