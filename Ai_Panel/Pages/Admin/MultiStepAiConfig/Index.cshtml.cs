using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.AiConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Panel.Pages.Admin.MultiStepAiConfig
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAiConfigRepository _aiConfigRepository;
        private readonly IGenericRepository<Domain.AiConfigGroup> _aiConfigGroup;

        public IndexModel(IAiConfigRepository aiConfigRepository , IGenericRepository<Domain.AiConfigGroup> aiConfigGroup)
        {
            _aiConfigRepository = aiConfigRepository;
            _aiConfigGroup = aiConfigGroup;
        }

        public List<Domain.AiConfig> AiConfigs { get; set; }
        public List<AiConfigGroupDto> AiConfigGroup { get; set; } 
        public async Task<IActionResult> OnGet()
        {
            AiConfigs = await _aiConfigRepository.GetAll(
                x => !x.IsDelete,
                x => x.OrderByDescending(o => o.Id),
                "AiModel,AiModel.Parent"
            );
            AiConfigGroup = AiConfigs
                .Where(x => x.AiConfigGroupId.HasValue)
                .GroupBy(x => x.AiConfigGroupId.Value)
                .Select(g => new AiConfigGroupDto
                {
                    GroupId = g.Key,
                    Title = g.First().Title,
                    Modles = g.Select(x => x.AiModel.Title).ToList(),
                })
                .ToList();


            return Page();
        }
    }

}
