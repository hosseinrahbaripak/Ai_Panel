using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Panel.Pages.User
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
