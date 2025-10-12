using Ai_Panel.Application.Constants;

namespace Ai_Panel.Classes
{
	public class PaymentHelper
    {
        private string _merchantType = "zarinpall";
        private IConfiguration _configuration;
        private SettingMethod _settingMethod;
        public PaymentHelper(IConfiguration configuration, SettingMethod settingMethod)
        { 
            _configuration = configuration;
            _settingMethod = settingMethod;
        }
        public async Task<string> Pay(string paymentId , int sum , string email , string mobile)
        {
            var url = _configuration["Url:Domain"];
            var address = url + "Payment/Verify?paymentId=" + paymentId;
            if (_settingMethod.IsLocal())
            {
                return await Local(sum, address, email, mobile);
            }
            switch (_merchantType)
            {
                case "zarinpall":
                    return await ZarinPall(sum, address, email, mobile);
                case "melli":
                    return await Melli(0, sum, address, email, mobile);
                default:
                    return "";
            }
        }
        private async Task<string> Local(int sum, string address, string email, string mobile)
        {
            var payment = new ZarinpalSandbox.Payment(sum);
            var res = payment.PaymentRequest(SystemMessages.BuyBook, address, email, mobile);
            if (res.Result.Status == 100)
            {
                return "https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority;
            }
            return "";
        }
        private async Task<string> ZarinPall(int sum, string address, string email, string mobile)
        { 
            var payment = new Zarinpal.Payment(_configuration["PrivateKey:Merchant"], sum);
            var res = payment.PaymentRequest(SystemMessages.BuyBook, address, email, mobile);
            if (res.Result.Status == 100)
            {
                return "https://zarinpal.com/pg/StartPay/" + res.Result.Authority;
            }
            return "";
        }
        private async Task<string> Melli(int addedPay, int sum, string address, string email, string mobile)
        {
            //PaymentRequest request = new PaymentRequest()
            //{
            //    OrderId = addedPay.ToString(),
            //    Amount = sum,
            //    MerchantId = _configuration["PrivateKey:Merchant_Melli"],
            //    TerminalId = _configuration["PrivateKey:TerminalId"],
            //    MerchantKey = _configuration["PrivateKey:TerminalKey"],
            //    ReturnUrl = address,
            //    PurchasePage = "https://sadad.shaparak.ir/"
            //};
            //var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}",
            //    request.TerminalId, request.OrderId, request.Amount));

            //var symmetric = SymmetricAlgorithm.Create("TripleDes");
            //symmetric.Mode = CipherMode.ECB;
            //symmetric.Padding = PaddingMode.PKCS7;

            //var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(request.MerchantKey), new byte[8]);

            //request.SignData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

            ////if (HttpContext.Request.Url != null)
            ////    request.ReturnUrl = string.Format("{0}://{1}{2}Home/Verify", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            //var ipgUri = string.Format("{0}/api/v0/Request/PaymentRequest", request.PurchasePage);


            ////HttpCookie merchantTerminalKeyCookie = new HttpCookie("Data", JsonConvert.SerializeObject(request));
            ////Response.Cookies.Add(merchantTerminalKeyCookie);

            //var data = new
            //{
            //    request.TerminalId,
            //    request.MerchantId,
            //    request.Amount,
            //    request.SignData,
            //    request.ReturnUrl,
            //    LocalDateTime = DateTime.Now,
            //    request.OrderId,
            //    //MultiplexingData = request.MultiplexingData
            //};

            //var res = ApiHelper.CallApi<PayResultData>(ipgUri, data);
            //res.Wait();

            //if (res != null && res.Result != null)
            //{
            //    if (res.Result.ResCode == "0")
            //    {
            //        return Redirect(string.Format("{0}/Purchase/Index?token={1}", request.PurchasePage, res.Result.Token));
            //    }
            //    return BadRequest();
            //}
            //return BadRequest();
            return "";
        }
    }
}
