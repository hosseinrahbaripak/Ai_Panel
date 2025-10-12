//using Live_Book.Application.Constants;
//using Live_Book.Application.Contracts.Persistence.EfCore;
//using Live_Book.Classes;
//using Live_Book.Infrastructure.Contracts.Notification;
//using Microsoft.AspNetCore.Mvc;

//namespace Live_Book.Controllers
//{
//    [ApiExplorerSettings(IgnoreApi = true)]
//    public class PaymentController : Controller 
//    {
//        private IConfiguration _configuration;
//        private SettingMethod _settingMethod;
//        ISmsManagerService _smsManager;
//        private ICart _cart;
//        public PaymentController(IConfiguration configuration, SettingMethod settingMethod, ISmsManagerService smsManager, ICart cart)
//        {
//            _configuration = configuration;
//            _settingMethod = settingMethod;
//            _smsManager = smsManager;
//            _cart = cart;
//        }

//        public async Task<IActionResult> Verify(string paymentId)
//        {
//            if (HttpContext.Request.Query["Status"] != "" &&
//                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
//                HttpContext.Request.Query["Authority"] != "")
//            {
//                            string authority = HttpContext.Request.Query["Authority"].ToString();

//                var cartPayment = await _cart.GetPay(paymentId);
//                if (cartPayment == null)
//                {
//                    ViewBag.error = SystemMessages.PaymentError;
//                    return View();
//                }

//                int status = 0;
//                string refId = "";
//                if (_settingMethod.IsLocal())
//                {
//                    var payment = new ZarinpalSandbox.Payment(cartPayment.Sum);
//                    var res = payment.Verification(authority).Result;
//                    status = res.Status;
//                    refId = res.RefId.ToString();
//                }
//                else
//                {
//                    var res = await ZarinPall(authority, cartPayment.Sum);
//                    status = res.Status;
//                    refId = res.RefId.ToString();
//                }
//                if (status != 100) return NotFound();

//                var submitPay = await _cart.VerifyPay(paymentId, refId);
//                if (submitPay)
//                {
//					var smsSuccessful = await _smsManager.PaymentSuccess(cartPayment.User.MobileNumber, cartPayment.User.Name, cartPayment.Sum.ToString());
//                    if (!smsSuccessful)
//					{
//						ViewBag.error = SystemMessages.SmsError;
//						return View();
//					}
//                    ViewBag.success = SystemMessages.PaymentSuccess;
//                    return View();
//                }

//            }

//            ViewBag.error = SystemMessages.PaymentError;
//            return View();
//        }
//        public async Task<Zarinpal.Models.PaymentVerificationResponse> ZarinPall(string authority, int sum)
//        {
//            var payment = new Zarinpal.Payment(_configuration["PrivateKey:Merchant"], sum);
//            var res = payment.Verification(authority).Result;
//            return res;
//        }
//    }
//}
