using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.TagHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Panel.Pages.Admin.Contract
{
    [PermissionChecker]
    public class IndexModel(IContractRepository contract) : PageModel
    {
        public List<Domain.ContractTemplate> Contracts { get; set; }

        public PagingTagHelper.PagingInfo PagingInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageId = 1)
        {
            int pageSize = 10;

            var allContracts = await contract.GetAll(where: c => !c.IsDelete);

            var totalItems = allContracts.Count;

            Contracts = allContracts
                .OrderByDescending(x => x.DateTime)
                .Skip((pageId - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            PagingInfo = new PagingTagHelper.PagingInfo
            {
                CurrentPage = pageId,
                ItemPerPage = pageSize,
                TotalItems = totalItems
            };

            return Page();
        }
    }
}
