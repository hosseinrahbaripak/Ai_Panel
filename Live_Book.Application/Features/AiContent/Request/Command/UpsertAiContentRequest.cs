using Live_Book.Application.DTOs.AiContent;
using MediatR;
using PersianAssistant.Models;

namespace Live_Book.Application.Features.AiContent.Request.Command;

public class UpsertAiContentRequest : IRequest<ServiceMessage>
{
    public AiContentUpsertDto Model { get; set; }
}