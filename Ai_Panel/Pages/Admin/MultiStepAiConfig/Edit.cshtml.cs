using System.Text.Json;
using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.AiConfig;
using Ai_Panel.Application.Features.AiConfig.Request.Command;
using Ai_Panel.Application.Features.AiConfig.Request.Queries;
using Ai_Panel.Classes;
using Ai_Panel.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ai_Panel.Pages.Admin.MultiStepAiConfig
{
    [Authorize]
    public class EditModel(IMediator mediator, IAiModelRepository aiModelRepository, IAiPlatformRepository aiPlatformRepository, IAiConfigRepository aiConfigGroup) : PageModel
    {
        public List<Domain.AiConfig> AiConfigDto { get; set; }
        public List<Domain.AiConfigGroup> Groups { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            AiConfigDto = await aiConfigGroup.GetAll(where: conf => conf.AiConfigGroupId == id, null, "AiModel,AiPlatform");
            AiConfigDto = AiConfigDto.OrderBy(x => x.AiConfigOrder).ToList();
            var aiModels = await aiModelRepository.GetAll(x => !x.IsDelete, null, "Platforms");
            var aiPlatforms = await aiPlatformRepository.GetAll(x => !x.IsDelete);
            var childModels = aiModels.Where(x => x.ParentId != null).ToList();
            var parentModels = aiModels.Where(x => x.ParentId == null).ToList();
            ViewData["AiPlatforms"] = JsonSerializer.Serialize(aiPlatforms.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
            }));
            ViewData["parentAiModels"] = JsonSerializer.Serialize(parentModels.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                ParentId = 0,
                PlatformIds = x.Platforms?.Select(x => x.Id),
            }));
            ViewData["childAiModels"] = JsonSerializer.Serialize(childModels.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                ParentId = x.ParentId,
            }));
            //FillDropDown();
            return Page();

        }

        //	public string Error { get; set; }
        //	[BindProperty]
        //	public UpsertAiConfigDto AiConfigDto { get; set; }
        //	//[BindProperty]
        //	//public AiContentUpsertDto AiContentDto { get; set; }
        //	[BindProperty]
        //	public bool WithAiContent { get; set; }

        //	public async Task<IActionResult> OnGet(int id)
        //	{
        //		AiConfigDto = await mediator.Send(new GetAiConfigRequest()
        //		{
        //			Id = id
        //		});

        //		//AiContentDto = await mediator.Send(new GetAiContentRequest()
        //		//{
        //		//	AiConfigId = id
        //		//});
        //		//WithAiContent = AiContentDto != null;
        //		if (AiConfigDto == null)
        //			return NotFound();
        //		await FillDropDown();
        //		return Page();
        //	}
        //	public async Task<IActionResult> OnPostAsync()
        //	{
        //		ModelState.Remove("AiConfigDto.AiModel");
        //		ModelState.Remove("AiConfigDto.IsDelete");
        //		ModelState.Remove("AiConfigDto.DateTime");
        //		ModelState.Remove("AiConfigDto.Version");
        //		ModelState.Remove("AiConfigDto.N");
        //		ModelState.Remove("AiConfigDto.CreateBy");
        //		if (!WithAiContent)
        //		{
        //			ModelState.Remove("AiContentDto.PartId");
        //			ModelState.Remove("AiContentDto.BookId");
        //			ModelState.Remove("AiContentDto.Content");
        //		}
        //		ModelState.Remove("AiContentDto.Id");
        //		ModelState.Remove("AiContentDto.Part");
        //		ModelState.Remove("AiContentDto.Book");
        //		ModelState.Remove("AiContentDto.IsDelete");
        //		ModelState.Remove("AiContentDto.DateTime");
        //		if (ModelState.IsValid)
        //		{
        //			//var admin = await adminHelper.GetAdminLoggedIn();
        //			//if (admin == null)
        //			//	return Page();

        //			//AiConfigDto.CreateBy = admin.AdminLoginId;
        //			var resAiConfig = await mediator.Send(new UpsertAiConfigRequest()
        //			{
        //				UpsertAiConfig = AiConfigDto
        //			});
        //			if (resAiConfig.ErrorId < 0)
        //			{
        //				Error = resAiConfig.ErrorTitle;
        //				return Page();
        //			}
        //			#region Edit AiContent
        //			//PersianAssistant.Models.ServiceMessage resAiContent;
        //			//if (WithAiContent)
        //			//{
        //			//	AiContentDto.AiConfigId = AiConfigDto.Id;
        //			//	resAiContent = await mediator.Send(new UpsertAiContentRequest()
        //			//	{
        //			//		Model = AiContentDto
        //			//	});
        //			//}
        //			//else
        //			//{
        //			//	resAiContent = await mediator.Send(new DeleteAiContentRequest()
        //			//	{
        //			//		AiConfigId = AiConfigDto.Id
        //			//	});
        //			//}
        //			//if (resAiContent.ErrorId < 0)
        //			//{
        //			//	Error = resAiContent.ErrorTitle;
        //			//	return Page();
        //			//}
        //			#endregion
        //			return Redirect(Urls.AiConfig);
        //		}
        //		await FillDropDown();
        //		return Page();
        //	}

        //	#region Utility

        private async Task FillDropDown()
        {
            var aiModels = await aiModelRepository.GetAll(x => !x.IsDelete, null, "Platforms");
            var aiPlatforms = await aiPlatformRepository.GetAll(x => !x.IsDelete);
            var childModels = aiModels.Where(x => x.ParentId != null).ToList();
            var parentModels = aiModels.Where(x => x.ParentId == null).ToList();
            ViewData["AiPlatforms"] = JsonSerializer.Serialize(aiPlatforms.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
            }));

            ViewData["AiPlatforms"] = new SelectList(await aiPlatformRepository.GetAll(x => !x.IsDelete), "Id", "Title");
            ViewData["parentAiModels"] = JsonSerializer.Serialize(parentModels.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                ParentId = 0,
                PlatformIds = x.Platforms?.Select(x => x.Id),
            }));
            ViewData["childAiModels"] = JsonSerializer.Serialize(childModels.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                ParentId = x.ParentId,
            }));
        }

        //	#endregion
        //}
    }
}
