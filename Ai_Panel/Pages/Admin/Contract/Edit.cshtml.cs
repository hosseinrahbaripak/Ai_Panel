using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.Contract;
using Ai_Panel.Application.Features.Contract.Request.Command;
using Ai_Panel.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Panel.Pages.Admin.Contract
{
    public class EditModel(IContractRepository contract , IMediator mediator) : PageModel
    {

        [BindProperty]
        public UpserContractDto ContractDto { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(int Id)
        {
            var res = await contract.Get(Id);
            ContractDto.Content = res.Content;
            ContractDto.Id = res.Id;
            ContractDto.Title = res.Title;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var res = await mediator.Send(new UpsertContractCommand()
            {
                Contract = ContractDto
            });
            if (res.ErrorId == 0)
            {
                return RedirectToPage("/Admin/Contract/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
