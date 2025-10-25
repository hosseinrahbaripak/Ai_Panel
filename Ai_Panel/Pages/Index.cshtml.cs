using Ai_Panel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Panel.Pages
{
    [PermissionChecker]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {   }
    }
}
