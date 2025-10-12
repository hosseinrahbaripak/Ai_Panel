using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ai_Panel
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private IRoleInPages _roleInPages;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await Task.CompletedTask;

            /*
            if (context.HttpContext.Request.Path.Value != null)
            {
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    var requestUrl = context.HttpContext.Request.Path.Value.Split("/").ToList();
                    if (requestUrl.Count == 2)
                        requestUrl.Add("Home");
                    if (requestUrl.Count == 3)
                        requestUrl.Add("Index");

                    var requestPage = requestUrl[2];
                    var requestAction = requestUrl[3];

                    _adminHelper = (AdminHelper)context.HttpContext.RequestServices.GetService(typeof(AdminHelper));
                    var adminLoggedIn = await _adminHelper.GetAdminLoggedIn();
                    if (adminLoggedIn == null)
                    {
                        context.Result = new RedirectResult("/Admin/SignOut");
                        return;
                    }
                    _roleInPages = (IRoleInPages)context.HttpContext.RequestServices.GetService(typeof(IRoleInPages));
                    var adminRoleHasAccessToThisPageAndAction = requestAction switch
                    {
                        "Index" => await _roleInPages.AnyAsync(x => x.RoleId == adminLoggedIn.RoleId && x.Pages.EnglishPageTitle == requestPage && x.Visit),
                        "Add" => await _roleInPages.AnyAsync(x => x.RoleId == adminLoggedIn.RoleId && x.Pages.EnglishPageTitle == requestPage && x.Add),
                        "Edit" => await _roleInPages.AnyAsync(x => x.RoleId == adminLoggedIn.RoleId && x.Pages.EnglishPageTitle == requestPage && x.Edit),
                        "Delete" => await _roleInPages.AnyAsync(x => x.RoleId == adminLoggedIn.RoleId && x.Pages.EnglishPageTitle == requestPage && x.Delete),
                        _ => false
                    };
                    if (!adminRoleHasAccessToThisPageAndAction)
                        context.Result = new RedirectResult("/NoPermission");

                }
                else
                {
                    context.Result = new RedirectResult("/Admin/Login");
                }
            }
            */
        }
    }
}
