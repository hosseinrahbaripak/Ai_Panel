
using Ai_Panel.Application.Features.User.Request.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Panel.Pages.Admin.User
{
    [Authorize]
    public class AddModel(IMediator mediator) : PageModel
    {
        [BindProperty]
        public Domain.User User { get; set; }


        
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (User != null)
            {
                var res = await mediator.Send(new UpsertUserRequest()
                {
                    User = User
                });

                return RedirectToPage("/Admin/User/Index");
            }
            return Page();
        }

    }
}
