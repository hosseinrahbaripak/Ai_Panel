using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.TestAiConfig;
using Ai_Panel.Application.Features.TestAiConfig.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Ai_Panel.Pages.Admin.TestAiConfigLogs;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IGenericRepository<Domain.TestAiConfig> _testAiConfigRepository;

    public IndexModel(IGenericRepository<Domain.TestAiConfig> testAiConfigRepository)
    {
        _testAiConfigRepository = testAiConfigRepository;
    }

    public List<Domain.TestAiConfig> TestAiConfigLogsDto { get; set; }

    public async Task OnGetAsync()
    {
        TestAiConfigLogsDto = await _testAiConfigRepository.GetAll(where: t => !t.IsDelete ,includeProperties: "User,AiModel" , orderBy: q => q.OrderByDescending(x => x.Id));
        await FillDropDown();
    }

    private async Task FillDropDown()
    {
        // ...
    }
}