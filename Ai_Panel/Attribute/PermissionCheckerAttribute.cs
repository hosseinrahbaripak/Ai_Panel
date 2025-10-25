using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Classes;
using Ai_Panel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ai_Panel
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectResult("/Login");
                return;
            }
            var user = context.HttpContext.User;
            var currentPath = context.HttpContext.Request.Path;
            var roles = user.FindFirst(CustomClaimTypes.Roles).Value;

            if ((currentPath.StartsWithSegments("/Admin") && (roles.Contains("Admin") || roles.Contains("SuperAdmin"))) ||
                (currentPath.StartsWithSegments("/User") && roles.Contains("User")))
            {
                return;
            }
            if (roles.Contains("Admin") || roles.Contains("SuperAdmin"))
            {
                context.Result = new RedirectResult("/Admin");
                return;
            }
            else if (roles.Contains("User"))
            {
                context.Result = new RedirectResult("/User");
                return;
            }




        }
    }
}
