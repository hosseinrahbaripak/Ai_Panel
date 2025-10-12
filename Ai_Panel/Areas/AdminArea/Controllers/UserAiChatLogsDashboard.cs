using Ai_Panel.Application.DTOs;
using Ai_Panel.Application.DTOs.AiChat;
using Ai_Panel.Application.Features.AiChat.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ai_Panel.Areas.AdminArea.Controllers;
[ApiExplorerSettings(IgnoreApi = true)]
[Route("Admin/UserAiChatLogsDashboard")]
[PermissionChecker]
[Area("AdminArea")]
public class UserAiChatLogsDashboard : Controller
{
    private readonly IMediator _mediator;
    public UserAiChatLogsDashboard(IMediator mediator)
    {
        _mediator = mediator;
    }
    //[Route("Index/FilterAiChatsCount")]
    //[HttpPost]
    //public async Task<IActionResult> FilterAiChatCounts(TimeBasedFilterDto modelFilter)
    //{
    //    var AiChatsCount = await _mediator.Send(new GetAiChatsCountRequest()
    //    {
    //        ModelFilter = modelFilter
    //    });
    //    return Json(new { titles = AiChatsCount.Titles, values = AiChatsCount.Values });
    //}
	//[Route("Index/FilterAiChatWordsCount")]
	//[HttpPost]
	//public async Task<IActionResult> FilterAiChatWordsCount(TimeBasedFilterDto modelFilter)
	//{
	//	var AiChatsCount = await _mediator.Send(new GetAiChatWordsCountRequest()
	//	{
	//		ModelFilter = modelFilter
	//	});
	//	return Json(new { titles = AiChatsCount.Titles, values = AiChatsCount.Values });
	//}
	//[Route("Index/FilterAiChatLogs")]
	//[HttpPost]
	//public async Task<IActionResult> FilterAiChatLogs(UserAiChatLogsFilter modelFilter)
	//{
	//	var AiChatlogs = await _mediator.Send(new GetUserAiChatLogsRequest()
	//	{
	//		ModelFilter = modelFilter
	//	});
	//	return PartialView(AiChatlogs);
	//}
}