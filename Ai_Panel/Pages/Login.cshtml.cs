using DNTCaptcha.Core;
using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs;
using Ai_Panel.Application.Tools;
using Ai_Panel.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Ai_Panel.Pages
{
    public class LoginModel : PageModel
    {
        #region Ctor 
        //private readonly DNTCaptchaOptions _captchaOptions;
        private readonly IUser _user;
        private readonly IJwtTokenGenerator _jwt;

        public LoginModel(IOptions<DNTCaptchaOptions> options, IUser user, IJwtTokenGenerator jwt)
        {
            //_captchaOptions = options == null ? throw new ArgumentNullException(nameof(options)) : options.Value;
            _user = user;
            _jwt = jwt;
        }

        #endregion

        #region Model
        [BindProperty]
        public LoginViewModel Login { get; set; }

        [BindProperty]
        public IFormCollection Form { get; set; }

        [BindProperty]
        public string? ReturnUrl { get; set; }
        #endregion

        public IActionResult OnGet(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/");
            }
            ReturnUrl = returnUrl;
            return Page();
        }
        public async Task<IActionResult> OnPostVerifyOtpAsync(string mobileNumber, string code)
        {
            try
            {
                var user = await _user.FirstOrDefault(x => !x.IsDelete && x.MobileNumber == mobileNumber, null, "UserRoles.Role");
                if (user == null)
                {
                    return new JsonResult(new { errorId = -1, result="" ,errorTitle = SystemMessages.UserNotFound });
                }

                bool isOtpValid = user.ActiveCode == code;

                if (!isOtpValid)
                {
                    return new JsonResult(new { errorId = -1, result = "", errorTitle = SystemMessages.ActiveCodeIncorrect });
                }

                var roles = string.Join(",", user.UserRoles.Select(ur => ur.Role.RoleTitle));
                string token = _jwt.GenerateToken(user.UserId);
                var claims = new List<Claim>
                {
                    new Claim(CustomClaimTypes.UserId, user.UserId.ToString()),
                    new Claim(CustomClaimTypes.Token, token),
                    new Claim(CustomClaimTypes.UserName, user.FirstName ?? ""),
                    new Claim(CustomClaimTypes.Roles, roles)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = false,
                };

                await HttpContext.SignInAsync(principal, properties);

                return new JsonResult(new
                {
                    errorId = 0,
                    result = SystemMessages.Success,
                    redirectUrl = roles.Contains("Admin") || roles.Contains("SuperAdmin") ? "/Admin" : "/User"
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new JsonResult(new { errorId = 1, result = "خطا در ورود" });
            }
        }
    }
}
