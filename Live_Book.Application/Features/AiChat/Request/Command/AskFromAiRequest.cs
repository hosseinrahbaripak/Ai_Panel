using Live_Book.Application.DTOs.AiChat;
using MediatR;
using PersianAssistant.Models;

namespace Live_Book.Application.Features.AiChat.Request.Command;

public class AskFromAiRequest : IRequest<ServiceMessage>
{
    public UpsertUserAiChatLogDto Model { get; set; }
    public bool GetFullData { get; set; }
}