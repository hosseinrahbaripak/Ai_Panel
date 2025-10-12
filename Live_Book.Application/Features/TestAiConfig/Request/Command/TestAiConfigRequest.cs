using Live_Book.Application.DTOs.AiConfig;
using MediatR;
using PersianAssistant.Models;

namespace Live_Book.Application.Features.TestAiConfig.Request.Command;

public class TestAiConfigRequest : IRequest<ServiceMessage>
{
    public TestAiConfigDto Model { get; set; }
}