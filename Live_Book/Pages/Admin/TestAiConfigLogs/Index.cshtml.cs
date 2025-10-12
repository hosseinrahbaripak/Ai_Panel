using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.TestAiConfig;
using Ai_Panel.Application.Features.TestAiConfig.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Ai_Panel.Pages.Admin.TestAiConfigLogs;

[PermissionChecker]
public class IndexModel(
	IMediator mediator,  IAdminManage adminManage,
	ITestAiConfigRepository TACRepository, IAiModelRepository aiModelRepository
) : PageModel
{
	public TestAiConfigDashboardDto TestAiConfigLogsDto { get; set; }
	public async Task OnGetAsync()
	{
		await FillDropDown();
		TestAiConfigLogsDto = await mediator.Send(new GetTestAiConfigLogsRequest());
	}
	private async Task FillDropDown()
	{
		//var adminsId = await TACRepository.GetAdminsId();
		//ViewData["Admins"] = new SelectList(await adminManage.GetAll(x => adminsId.Contains(x.LoginID)), "LoginID", "UserName");

		//var aiModels = await aiModelRepository.GetAll(x => !x.IsDelete);
		//var childModels = aiModels.Where(x => x.ParentId != null).ToList();
		//ViewData["childAiModels"] = JsonSerializer.Serialize(childModels);
		//ViewData["AiTypes"] = new SelectList(aiModels.Where(x => x.ParentId == null), "Id", "Title");

		//var bookParts = await bookPartRepository.GetBookParts(x => !x.IsDelete && x.AiContentId != null);
		//ViewData["Books"] = new SelectList(await bookRepository.GetAllBooks(x => x.BookParts.Any(y => y.AiContentId != null)), "Id", "Name");
		//ViewData["BookParts"] = JsonSerializer.Serialize(bookParts.Select(x => new
		//{
		//	Id = x.PartId,
		//	BookId = x.BookId,
		//	Title = x.PartName
		//}));
	}
}