using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Panel.Pages.Admin.AiConfig
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        private readonly IAiConfigRepository _aiConfigRepository;

        public IndexModel(IAiConfigRepository aiConfigRepository)
        {
            _aiConfigRepository = aiConfigRepository;
        }

        public List<Domain.AiConfig> AiConfigs { get; set; }

        public async Task<IActionResult> OnGet()
        {
            AiConfigs = await _aiConfigRepository.GetAll(
                x => !x.IsDelete,
                x => x.OrderByDescending(o => o.Id),
                "AiModel,AiModel.Parent"
            );

            return Page();
        }
    }
}
