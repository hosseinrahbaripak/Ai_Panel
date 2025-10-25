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

namespace Ai_Panel.Pages.Admin
{
    public class LoginModel : PageModel
    {
        #region Ctor 
        //private readonly DNTCaptchaOptions _captchaOptions;
        private readonly IUser _user;
        private readonly IJwtTokenGenerator _jwt;

        public LoginModel(IOptions<DNTCaptchaOptions> options , IUser user , IJwtTokenGenerator jwt)
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
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Login.MobileNumber", "اطلاعات صحیح نیست");
                return Page();
            }
            //if (!_validatorService.HasRequestValidCaptchaEntry())
            //{
            //    ModelState.AddModelError(_captchaOptions.CaptchaComponent.CaptchaInputName, SystemMessages.CaptchaError);
            //    return Page();
            //}

            var user = await _user.FirstOrDefault(x => x.MobileNumber == Login.MobileNumber);
            if (user == null)
            {
                ModelState.AddModelError("Login.MobileNumber", "اطلاعات صحیح نیست");
                return Page();
            }

            var pass = Login.Password.GeneratePass(user.PassKey).Item2;
            if (user.Password != pass)
            {
                ModelState.AddModelError("Login.Password", "رمز عبور صحیح نیست");
                return Page();
            }
            else
            {
                try
                {
                    string token = _jwt.GenerateToken(user.UserId);
                    var clamis = new List<Claim>
                        {
                            new Claim(CustomClaimTypes.UserId,user.UserId.ToString()),
                            new Claim(CustomClaimTypes.Token ,token),
                            new Claim(CustomClaimTypes.UserName ,user.FirstName),
                            new Claim(CustomClaimTypes.Email,user.Email),
                        };

                    var identity = new ClaimsIdentity(clamis, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = Login.RememberMe,
                    };

                    await HttpContext.SignInAsync(principal, properties);

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    
                }

                return RedirectToPage("/");
            }
        }
    }
}
