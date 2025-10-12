using Ai_Panel.Application.DTOs.AiConfig;
using MediatR;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.TestAiConfig.Request.Command;

public class TestAiConfigRequest : IRequest<ServiceMessage>
{
    public TestAiConfigDto Model { get; set; }
}