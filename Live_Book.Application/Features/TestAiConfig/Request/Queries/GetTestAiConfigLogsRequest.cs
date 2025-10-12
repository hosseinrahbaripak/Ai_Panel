using Live_Book.Application.DTOs.TestAiConfig;
using MediatR;

namespace Live_Book.Application.Features.TestAiConfig.Request.Queries;
public class GetTestAiConfigLogsRequest : IRequest<TestAiConfigDashboardDto>
{
	public TestAiConfigFilter? ModelFilter { get; set; }
}

