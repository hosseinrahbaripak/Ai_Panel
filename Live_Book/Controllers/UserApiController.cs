//using Ai_Panel.Application.Constants;
//using Ai_Panel.Application.Contracts.Persistence.EfCore;
//using Ai_Panel.Application.DTOs;
//using Microsoft.AspNetCore.Mvc;
//using PersianAssistant.Extensions;
//using PersianAssistant.Models;

//namespace Ai_Panel.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	[IgnoreAntiforgeryToken]
//	public class UserApiController : ControllerBase
//	{
//		private readonly IUser _userRepository;
//		private readonly IFrequentlyQuestion _frequentlyQuestion;
//		private readonly ILearnApp _learnApp;
//		private ITicket _ticket;
//		private readonly IRequestLogin _requestLogin;

//		public UserApiController(IUser userRepository, IFrequentlyQuestion frequentlyQuestion, ILearnApp learnApp, ITicket ticket, IRequestLogin requestLogin)
//		{
//			_userRepository = userRepository;
//			_frequentlyQuestion = frequentlyQuestion;
//			_learnApp = learnApp;
//			_ticket = ticket;
//			_requestLogin = requestLogin;
//		}

//		[HttpPost]
//		[Route("Login")]
//		public async Task<ActionResult<ServiceMessage>> Login(LoginViewModel loginViewModel)
//		{
//			//try
//			//{
//			//    var filter = await FilterRequest(loginViewModel);
//			//    if (filter != null) return filter;

//			//    var mobile = loginViewModel.MobileNumber.ToMobileNumber();
//			//    if (string.IsNullOrEmpty(mobile))
//			//    {
//			//        return ResponseManager.DataError(SystemMessages.MobileIncorrect);
//			//    }

//			//    var code = await _userRepository.Login(mobile);
//			//    await _smsManager.Login(mobile, code);
//			//    return ResponseManager.FillObject(SystemMessages.ActiveCodeSms);
//			//}
//			//catch (Exception)
//			//{
//			//    return ResponseManager.ServerError();
//			//}
//			return null;
//		}

//		[HttpPost]
//		[Route("Activation")]
//		public async Task<ActionResult<ServiceMessage>> Activation(Activation activation)
//		{
//			try
//			{
//				var mobile = activation.MobileNumber.ToIranMobileNumber();
//				if (string.IsNullOrEmpty(mobile))
//				{
//					return ResponseManager.DataError(SystemMessages.MobileIncorrect);
//				}
//				var res = await _userRepository.Activation(mobile, activation.ActiveCode);
//				var requestLogin = await _requestLogin.LastOrDefault(x => x.PhoneNumber == mobile);
//				if (res != null)
//				{
//					if (requestLogin != null)
//					{
//						if (requestLogin.ActiveCode == activation.ActiveCode)
//						{
//							requestLogin.Status = 2;
//							requestLogin.DateReq = DateTime.UtcNow.AddHours(3.5);
//							await _requestLogin.Update(requestLogin);
//						}
//					}
//					return ResponseManager.FillObject(res);
//				}
//				else
//				{
//					if (requestLogin != null)
//					{
//						if (requestLogin.ActiveCode != activation.ActiveCode)
//						{
//							requestLogin.Status = 5;
//							requestLogin.DateReq = DateTime.UtcNow.AddHours(3.5);
//							await _requestLogin.Update(requestLogin);
//						}
//					}
//					return ResponseManager.DataError(SystemMessages.ActiveCodeIncorrect);
//				}
//			}
//			catch (Exception e)
//			{
//				return ResponseManager.ServerError();
//			}
//		}
//		[HttpPost]
//		[Route("UpdateProfile")]
//		public async Task<ActionResult<ServiceMessage>> UpdateProfile(UserViewModel model)
//		{
//			try
//			{
//				//var filter = await FilterRequest(model);
//				//if (filter != null) return filter;

//				var token = GetToken();
//				if (string.IsNullOrEmpty(token))
//				{
//					return ResponseManager.DataError(SystemMessages.TokenError);
//				}

//				var res = await _userRepository.UpdateProfile(token, model);
//				return res != null ? ResponseManager.FillObject(res) : ResponseManager.DataError(SystemMessages.ActiveCodeIncorrect);

//			}
//			catch (Exception)
//			{
//				return ResponseManager.ServerError();
//			}
//		}

//		[HttpGet]
//		[Route("Logout")]
//		public async Task<ActionResult<ServiceMessage>> Logout()
//		{
//			try
//			{
//				string token = GetToken();
//				// اگه توکن خالی باشه اجرا نمیشه و ارور دیتا میده
//				if (!string.IsNullOrEmpty(token))
//				{
//					var session = await _userRepository.GetByToken(token);
//					if (session != null)
//					{
//						await _userRepository.DeActiveSession(session.Id);
//						return ResponseManager.FillObject(SystemMessages.Success);
//					}
//					else
//					{
//						return ResponseManager.DataError("User Not Found");
//					}
//				}
//				else
//				{
//					return ResponseManager.DataError("توکن ورودی خالی است");
//				}
//			}
//			catch (Exception)
//			{
//				return ResponseManager.ServerError();
//			}
//		}

//		[HttpGet]
//		[Route("FrequentlyQuestion")]
//		public async Task<ActionResult<ServiceMessage>> FrequentlyQuestion()
//		{
//			try
//			{
//				var res = await _frequentlyQuestion.GetAll();
//				return ResponseManager.FillObject(res);
//			}
//			catch (Exception e)
//			{
//				return ResponseManager.ServerError();
//			}
//		}

//		[HttpGet]
//		[Route("LearnApp")]
//		public async Task<ActionResult<ServiceMessage>> LearnApp()
//		{
//			try
//			{
//				var res = await _learnApp.GetLearnAppForApi();
//				return ResponseManager.FillObject(res);
//			}
//			catch (Exception e)
//			{
//				return ResponseManager.ServerError();
//			}
//		}

//		#region --utility--  
//		private string GetRealIp()
//		{
//			if (!string.IsNullOrEmpty(HttpContext.Request.Headers["AR_REAL_IP"]))
//				return HttpContext.Request.Headers["AR_REAL_IP"];
//			if (!string.IsNullOrEmpty(HttpContext.Request.Headers["REMOTE_ADDR"]))
//				return HttpContext.Request.Headers["REMOTE_ADDR"];
//			return HttpContext.Connection.RemoteIpAddress?.ToString();
//		}
//		private string GetToken()
//		{
//			var header = HttpContext.Request.Headers["Token"];
//			return string.IsNullOrEmpty(header) ? "" : header.ToString();
//		}
//		private async Task<string> GetFileFromBucket(string fileName)
//		{
//			HttpClient client = new HttpClient();
//			client.DefaultRequestHeaders.Add("AwsBucketName", "livebook");
//			HttpResponseMessage response = await client
//				.GetAsync($"http://file.mhelli.com/api/GetFile?name={fileName}&timeout=15");
//			var res = await response.Content.ReadAsStringAsync();
//			return res;
//		}
//		#endregion
//	}
//}
