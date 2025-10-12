using Ai_Panel.Application.DTOs.TestAiConfig;
using MediatR;

namespace Ai_Panel.Application.Features.TestAiConfig.Request.Queries;
public class GetTestAiConfigLogsRequest : IRequest<TestAiConfigDashboardDto>
{
	public TestAiConfigFilter? ModelFilter { get; set; }
}

