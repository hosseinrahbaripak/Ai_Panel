using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.AiConfig;
using Ai_Panel.Application.Features.AiConfig.Request.Command;
using Ai_Panel.Classes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Ai_Panel.Pages.Admin.AiConfig
{
    [Authorize]
    public class AddModel(IMediator mediator, IAiModelRepository aiModelRepository,
		IAiConfigRepository aiConfigRepository, IAiPlatformRepository aiPlatformRepository) : PageModel
	{
		public string Error { get; set; }
		[BindProperty]
		public UpsertAiConfigDto AiConfigDto { get; set; }
		[BindProperty]
		public bool WithAiContent { get; set; }
		public async Task<IActionResult> OnGet()
		{
			await FillDropDown();
			return Page();
		}
		public async Task<IActionResult> OnPostAsync()
		{
			ModelState.Remove("AiConfigDto.IsDelete");
			ModelState.Remove("AiConfigDto.DateTime");
			ModelState.Remove("AiConfigDto.Version");
			ModelState.Remove("AiConfigDto.N");
			ModelState.Remove("AiConfigDto.CreateBy");
			if (!WithAiContent)
			{
				ModelState.Remove("AiContentDto.PartId");
				ModelState.Remove("AiContentDto.BookId");
				ModelState.Remove("AiContentDto.Content");
			}
			ModelState.Remove("AiContentDto.Part");
			ModelState.Remove("AiContentDto.Book");
			ModelState.Remove("AiContentDto.IsDelete");
			ModelState.Remove("AiContentDto.DateTime");
			if (ModelState.IsValid)
			{
				//var admin = await adminHelper.GetAdminLoggedIn();
				//if (admin == null)
				//	return Redirect(Urls.SignOut);
				//AiConfigDto.CreateBy = admin.AdminLoginId;
				var resAiConfig = await mediator.Send(new UpsertAiConfigRequest()
				{
					UpsertAiConfig = AiConfigDto
				});
				if (resAiConfig.ErrorId < 0)
				{
					Error = resAiConfig.ErrorTitle;
					return Page();
				}
				return Redirect(Urls.AiConfig);
			}
			await FillDropDown();
			return Page();
		}

		#region Utility
		
		private async Task FillDropDown()
		{
            var aiModels = await aiModelRepository.GetAll(x => !x.IsDelete, null, "Platforms");
            var selectedAiModel = AiConfigDto?.AiModelId > 0 ? aiModels.Find(x => x.Id == AiConfigDto?.AiModelId) : null;
			var childModels = aiModels.Where(x => x.ParentId != null).ToList();
			var parentModels = aiModels.Where(x => x.ParentId == null).ToList();
			ViewData["AiPlatforms"] = new SelectList(await aiPlatformRepository.GetAll(x => !x.IsDelete), "Id", "Title", AiConfigDto?.AiPlatformId);
			ViewData["parentAiModels"] = JsonSerializer.Serialize(parentModels.Select(x => new
			{
				Id = x.Id,
				Title = x.Title,
				ParentId = 0,
				PlatformIds = x.Platforms?.Select(x => x.Id),
				Selected = selectedAiModel?.ParentId
			}));
			ViewData["childAiModels"] = JsonSerializer.Serialize(childModels.Select(x => new
			{
				Id = x.Id,
				Title = x.Title,
				ParentId = x.ParentId,
				Selected = AiConfigDto?.AiModelId
			}));
		}
		#endregion
	}
}
