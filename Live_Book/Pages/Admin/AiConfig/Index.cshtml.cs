using Live_Book.Application.Contracts.Persistence.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Live_Book.Pages.Admin.AiConfig
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
