using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Application.DTOs;
using Live_Book.Application.DTOs.AiChat;
using Live_Book.Application.Enum;
using Live_Book.Application.Features.AiChat.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Live_Book.Pages.Admin.UserAiChatLogsDashboard;

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

