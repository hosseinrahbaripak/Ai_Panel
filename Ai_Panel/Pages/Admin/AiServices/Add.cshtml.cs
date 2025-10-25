using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.AiConfig;
using Ai_Panel.Application.DTOs.Aiservice;
using Ai_Panel.Application.Features.AiConfig.Request.Queries;
using Ai_Panel.Application.Features.AiServices.Request.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ai_Panel.Pages.Admin.AiServices
{
    [PermissionChecker]
    public class AddModel(IMediator mediator , IAiConfigRepository aiConfig) : PageModel
    {
        [BindProperty]
        public UpsertAiServiceDto AiService { get; set; }
        [BindProperty]
        public List<GetAiConfigsDto> AiConfigs { get; set; } 
        public async Task<IActionResult> OnGetAsync()
        {
            AiConfigs = await mediator.Send(new GetAiConfigsRequest() { });
            await FillDropDown();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await mediator.Send(new UpserAiServiceRequest()
            {
                AiService = AiService
            });
            await FillDropDown();
            return Redirect("/Admin/AiServices");
        }

        public async Task FillDropDown()
        {
            ViewData["AiConfigs"] = new SelectList(AiConfigs, "Id", "Title");
        }
    }
}
