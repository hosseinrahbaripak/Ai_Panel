//using Live_Book.Application.Contracts.Persistence.EfCore;
//using Live_Book.Application.DTOs.AppLogin;
//using Live_Book.Application.Features.AppLogin.Request.Command;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using PersianAssistant.Extensions;
//using PersianAssistant.Models;

//namespace Live_Book.Controllers.api4
//{
//	[Route("api4/[controller]")]
//	[ApiController]
//	[IgnoreAntiforgeryToken]
//	public class UserApiController : ControllerBase
//	{
//		private readonly IMediator _mediator;
//		private readonly IErrorLog _log;

//		public UserApiController(IMediator mediator, IErrorLog log)
//		{
//			_mediator = mediator;
//			_log = log;
//		}

//		[Route("Login")]
//		[HttpPost]
//		public async Task<ActionResult<ServiceMessage>> Login(AppLoginV4Dto loginViewModel)
//		{
//			try
//			{
//				var res = await _mediator.Send(new AppLoginV4Request()
//				{
//					Model = loginViewModel,
//					CreateUser = false,
//					InBookShop = false
//				});
//				return res;
//			}
//			catch (Exception e)
//			{
//				await _log.Add(e.Message, e.InnerException?.Message ?? "", "Login In Api V3");
//				return ResponseManager.ServerError();
//			}
//		}

//		[HttpPost]
//		[Route("Activation")]
//		public async Task<ActionResult<ServiceMessage>> Activation(AppActivationV4Dto activation)
//		{
//			try
//			{
//				var res = await _mediator.Send(new ActivationV4Request()
//				{
//					Model = activation,
//					InBookShop = false
//				});
//				return res;
//			}
//			catch (Exception e)
//			{
//				await _log.Add(e.Message, e.InnerException?.Message ?? "", "Login In Api V3");
//				return ResponseManager.ServerError();
//			}
//		}
//	}
//}
