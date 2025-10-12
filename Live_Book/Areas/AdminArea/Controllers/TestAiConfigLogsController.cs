using Ai_Panel.Application.DTOs.TestAiConfig;
using Ai_Panel.Application.Features.TestAiConfig.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ai_Panel.Areas.AdminArea.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("Admin/TestAiConfigLogs")]
[PermissionChecker]
[Area("AdminArea")]
public class TestAiConfigLogsController(IMediator mediator) : Controller
{
	[Route("Index/FilterTestAiConfigLogs")]
	[HttpPost]
	public async Task<IActionResult> FilterTestAiConfigLogs(TestAiConfigFilter modelFilter)
	{
		var testAiConfigLogs = await mediator.Send(new GetTestAiConfigLogsRequest()
		{
			ModelFilter = modelFilter
		});
		return PartialView(testAiConfigLogs);
	}
}