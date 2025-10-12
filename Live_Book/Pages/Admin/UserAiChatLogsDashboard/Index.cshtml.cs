using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs;
using Ai_Panel.Application.DTOs.AiChat;
using Ai_Panel.Application.Enum;
using Ai_Panel.Application.Features.AiChat.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ai_Panel.Pages.Admin.UserAiChatLogsDashboard;

[PermissionChecker]
public class IndexModel(IMediator mediator,  IUser userRepository, IUserAiChatLogsRepository UACLRepository) : PageModel
{
	public AiChatDashboardDto AiChatLogsDashboardDto { get; set; }
	public DateValueDto AiChatsCounts { get; set; }
	public DateValueDto AiChatWordsCount { get; set; }
	public async Task OnGetAsync()
	{
		await FillDropDown();
		//var modelFilter = new TimeBasedFilterDto()
		//{
		//	TimeType = TimeType.Weekly,
		//};
		//AiChatLogsDashboardDto = await mediator.Send(new GetUserAiChatLogsRequest());
		//AiChatsCounts = await mediator.Send(new GetAiChatsCountRequest() { ModelFilter = modelFilter});
		//AiChatWordsCount = await mediator.Send(new GetAiChatWordsCountRequest() { ModelFilter = modelFilter });
	}
	private async Task FillDropDown()
	{
		//var selectedTimeTypeId = (int)TimeType.Weekly;
		//ViewData["TimeType"] = new SelectList(Application.Tools.Utility.GetTimeTypeIdTitle(), "Id", "Title", selectedTimeTypeId);
		//ViewData["Books"] = new SelectList(await bookRepository.GetAllBooks(x => x.BookParts.Any(y=> y.HasAiContent)), "Id", "Name");
		//var usersId = await UACLRepository.GetUsersId();
		//ViewData["Users"] = new SelectList(await userRepository.GetAll(x => usersId.Contains(x.UserId)), "UserId", "Name");
	}
}

