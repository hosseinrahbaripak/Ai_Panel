using Ai_Panel.Application.DTOs.Contract;
using Ai_Panel.Application.Features.Contract.Request.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Ai_Panel.Pages.Admin.Contract
{
    public class AddModel(IMediator mediator) : PageModel
    {
        [BindProperty]
        public UpserContractDto ContractDto { get; set; } 
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var res = await mediator.Send(new UpsertContractCommand()
            {
                Contract = ContractDto
            });
            if(res.ErrorId == 0)
            {
                return RedirectToPage("/Admin/Contract");
            }
            else
            {
                return Page();
            }
        }
    }
}
