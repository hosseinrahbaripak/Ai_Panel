using Ai_Panel.Application.DTOs.AiChat;
using MediatR;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.AiChat.Request.Command;

public class AskFromAiRequest : IRequest<ServiceMessage>
{
    public UpsertUserAiChatLogDto Model { get; set; }
    public bool GetFullData { get; set; }
}