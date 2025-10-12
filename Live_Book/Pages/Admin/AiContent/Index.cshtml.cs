using Live_Book.Application.Contracts.Persistence.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Live_Book.Pages.Admin.AiContent
{
    public class IndexModel(IAiContentRepository aiContentRepository) : PageModel
    {
        public List<Domain.AiContent> AiContents { get; set; }
        public async Task<IActionResult> OnGet()
        {
            AiContents = await aiContentRepository.GetAll(x => !x.IsDelete, x => x.OrderByDescending(o => o.Id), "AiConfig,Book,Part,AiConfig.AiModel,AiConfig.AiModel.Parent");
            return Page();
        }
    }
}
