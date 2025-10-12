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
        private readonly IAdminManage _adminManage;
        private readonly IRoleInPages _roleInPages;
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly DNTCaptchaOptions _captchaOptions;

        public LoginModel(IAdminManage adminManage, IRoleInPages roleInPages, IDNTCaptchaValidatorService validatorService, IOptions<DNTCaptchaOptions> options)
        {
            _adminManage = adminManage;
            _roleInPages = roleInPages;
            _validatorService = validatorService;
            _captchaOptions = options == null ? throw new ArgumentNullException(nameof(options)) : options.Value;
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
                return RedirectToPage("/Admin/Home/Index");
            }
            ReturnUrl = returnUrl;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //var captchaResponse = await ReCaptcha.GoogleRecaptcha(Form);
            //if (captchaResponse == false)
            //{
            //    ViewData["Message"] = "کپچا اشتباه بود لطفا دوباره امتحان کنید.";
            //    return Page();
            //}

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Login.UserName", "اطلاعات صحیح نیست");
                return Page();
            }
            if (!_validatorService.HasRequestValidCaptchaEntry())
            {
                ModelState.AddModelError(_captchaOptions.CaptchaComponent.CaptchaInputName, SystemMessages.CaptchaError);
                return Page();
            }
            var admin = await _adminManage.FirstOrDefault(x => x.UserName == Login.UserName.ToLower());
            if (admin == null)
            {
                ModelState.AddModelError("Login.UserName", "اطلاعات صحیح نیست");
                return Page();
            }
            else
            {
                var pass = Login.Password.GeneratePass(admin.Key);
                if (pass.Item2 != admin.Password)
                {
                    ModelState.AddModelError("Login.UserName", "اطلاعات صحیح نیست");
                    return Page();
                }
                else
                {
                    try
                    {
                        int? adminProfileId = await _adminManage.GetAdminProfileId(admin.LoginID);
                        var pages = await _roleInPages.GetAll(x => x.RoleId == admin.RoleId, null, "Pages");
                        var clamis = new List<Claim>
                        {
                            new Claim(CustomClaimTypes.AdminLoginId,admin.LoginID.ToString()),
                            new Claim(CustomClaimTypes.UserName,admin.UserName),
                            new Claim(CustomClaimTypes.Email,admin.Email),
                            new Claim(CustomClaimTypes.RoleId,admin.RoleId.ToString()),
                            new Claim(CustomClaimTypes.AdminType,admin.Role.AdminTypeId?.ToString() ?? 0.ToString()),
                            new Claim(CustomClaimTypes.IsSuperAdmin,admin.IsSuperAdmin.ToString()),
                            new Claim(CustomClaimTypes.Pages,string.Join(",",pages.Select(x=>x.Pages.PageAddress))),
                            new Claim(CustomClaimTypes.AdminProfileId, adminProfileId.ToString() ?? string.Empty)
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

                    return RedirectToPage("/Admin/Home/Index");
                }
            }
            //برای ساخت ادمین جدید از این طریق استفاده می شود
            //var password = login.Password.GeneratePass();
            //_Context.Add(new Model.AdminLogin()
            //{
            //    Key = password.Item1,
            //    Password = password.Item2,
            //    UserName = login.UserName
            //});

            //var user = _Context.GetAdminForLogin
            //    (login.UserName.ToLower(), login.Password);

            //if (user == null)
            //{
            //    ModelState.AddModelError("UserName", "اطلاعات صحیح نیست");
            //    return View(login);
            //}
        }
    }
}
