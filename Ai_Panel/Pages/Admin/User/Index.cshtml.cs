using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Panel.Pages.Admin.User
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private IUser _user;

        public IndexModel(IUser user)
        {
            _user = user;
        }

        [BindProperty]
        public List<Domain.User> Users { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Users = await _user.GetAll();
            return Page();
        }
    }
}
