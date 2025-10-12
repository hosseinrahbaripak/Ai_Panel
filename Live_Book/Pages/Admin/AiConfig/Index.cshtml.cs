using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Panel.Pages.Admin.AiConfig
{
    public class IndexModel(IAiConfigRepository aiConfigRepository) : PageModel
    {

        //public List<Domain.AiConfig> AiConfigs { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //AiConfigs = await aiConfigRepository.GetAll(x => !x.IsDelete,x=>x.OrderByDescending(o=>o.Id), "AiModel,AiModel.Parent");
            return Page();
        }
    }
}
