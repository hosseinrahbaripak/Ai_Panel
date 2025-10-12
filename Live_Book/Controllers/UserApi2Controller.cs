//using Live_Book.Application.Constants;
//using Live_Book.Application.Contracts.Persistence.EfCore;
//using Live_Book.Application.DTOs;
//using Live_Book.Domain;
//using Live_Book.Infrastructure.Contracts.Notification;
//using Microsoft.AspNetCore.Mvc;
//using PersianAssistant.Extensions;
//using PersianAssistant.Models;

//namespace Live_Book.Controllers
//{
//    [Route("api2/[controller]")]
//    [ApiController]
//    [IgnoreAntiforgeryToken]
//    public class UserApi2Controller : ControllerBase
//    {
//        private readonly IUser _user;
//        private readonly ISmsManagerService _smsManager;
//        private readonly IRequestLogin _requestLogin;
//        public UserApi2Controller(IUser user, ISmsManagerService smsManager, IRequestLogin requestLogin, IAppVersion appVersion)
//        {
//            _user = user;
//            _smsManager = smsManager;
//            _requestLogin = requestLogin;
//            _appVersion = appVersion;
//        }

//        [HttpPost]
//        [Route("Login")]
//        public async Task<ActionResult<ServiceMessage>> Login(UserLoginViewModel loginViewModel, string? versionType = "")
//        {
//            var requestLogin = new RequestLogin();
//            requestLogin.DateReq = DateTime.UtcNow.AddHours(3.5);
//            try
//            {
//                if (!string.IsNullOrEmpty(versionType))
//                {
//                    var res = await CheckAppVersion(versionType);
//                    if (res is not null)
//                    {
//                        return res;
//                    }
//                }
//                var mobile = loginViewModel.MobileNumber.ToIranMobileNumber();
//                if (string.IsNullOrEmpty(mobile))
//                {
//                    requestLogin.PhoneNumber = loginViewModel.MobileNumber != null && loginViewModel.MobileNumber.Length <= 50 ? loginViewModel.MobileNumber : "";
//                    requestLogin.Status = 4;
//                    await _requestLogin.Add(requestLogin);
//                    return ResponseManager.DataError(SystemMessages.MobileIncorrect);
//                }

//                requestLogin.PhoneNumber = mobile;
//                var user = await _user.FirstOrDefault(x => x.MobileNumber == mobile);
//                if (user != null)
//                {
//                    var code = await _user.Login(mobile);
//					var smsSuccessful = await _smsManager.Login(mobile, code);
//					if (!smsSuccessful)
//					{
//						requestLogin.Status = 0;
//						await _requestLogin.Add(requestLogin);
//						return ResponseManager.DataError(SystemMessages.SmsError);
//					}
//					requestLogin.UserId = user.UserId;
//                    requestLogin.User = user;
//                    requestLogin.Status = 1;
//                    requestLogin.ActiveCode = code;
//                    await _requestLogin.Add(requestLogin);
//                    return ResponseManager.FillObject(SystemMessages.ActiveCodeSms);
//                }
//                else
//                {
//                    requestLogin.Status = 0;
//                    await _requestLogin.Add(requestLogin);
//                    return ResponseManager.DataError(SystemMessages.NoNewUser);
//                }
//            }
//            catch (Exception e)
//            {
//                requestLogin.Status = 3;
//                var mobile = requestLogin.PhoneNumber;
//                requestLogin.PhoneNumber = mobile ?? "";
//                await _requestLogin.Add(requestLogin);
//                return ResponseManager.ServerError();
//            }
//        }

//        #region Utility
//        private string GetRealIp()
//        {
//            if (!string.IsNullOrEmpty(HttpContext.Request.Headers["AR_REAL_IP"]))
//                return HttpContext.Request.Headers["AR_REAL_IP"];
//            if (!string.IsNullOrEmpty(HttpContext.Request.Headers["REMOTE_ADDR"]))
//                return HttpContext.Request.Headers["REMOTE_ADDR"];
//            return HttpContext.Connection.RemoteIpAddress?.ToString();
//        }
//        private string GetToken()
//        {
//            var header = HttpContext.Request.Headers["Token"];
//            return string.IsNullOrEmpty(header) ? "" : header.ToString();
//        }
//        private async Task<ServiceMessage?> CheckAppVersion(string appVersion = "")
//        {
//            Tuple<bool, string>? isVersionExpire = await _appVersion.IsVersionExpire(appVersion);
//            if (isVersionExpire is null)
//            {
//                return ResponseManager.DataError("لطفا نوع ورژن را درست وارد کنید !");
//            }
//            if (isVersionExpire.Item1)
//            {
//                return ResponseManager.CustomResponse(-11, "آپدیت جدیدی موجود است", isVersionExpire.Item2);
//            }
//            return null;
//        }

//        #endregion
//    }
//}
