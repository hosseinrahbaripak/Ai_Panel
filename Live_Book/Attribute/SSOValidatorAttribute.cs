using Ai_Panel.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ai_Panel.Attribute;
public class SSOValidatorAttribute : System.Attribute,  IAsyncAuthorizationFilter
{
    private ISsoService _ssoService;
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
	{
		try
		{
            var headers = context.HttpContext.Request.Headers;
            if (!headers.TryGetValue("Authorization", out var authorization) ||
                string.IsNullOrWhiteSpace(authorization))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            if (string.IsNullOrEmpty(authorization))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            _ssoService = context.HttpContext.RequestServices.GetService<ISsoService>();
            var isValid = await _ssoService.ValidateToken(authorization);
			if (!isValid)
			{
				context.Result = new ForbidResult();
				return;
			}
		}
		catch(Exception e)
		{
			return;
		}
	}
}