using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.AiChat;
using Ai_Panel.Application.DTOs.User;
using Ai_Panel.Application.Features.User.Request.Command;
using Ai_Panel.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersianAssistant.Extensions;
using PersianAssistant.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ai_Panel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "JwtBearer")]
    public class AuthController : Controller
    {
        private readonly IUser _user;
        private readonly IMediator _mediator;
        public AuthController(IUser user, IMediator mediator)
        {
            _user = user;
            _mediator = mediator;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<ServiceMessage>> Register(RegisterUserDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return new ServiceMessage()
                {
                    ErrorId = -1,
                    Result = errors,
                    ErrorTitle = "داده‌های ورودی معتبر نیستند"
                };
            }
            bool isUserExist = await _user.Any(where: u => u.MobileNumber == model.MobileNumber || u.NationalId == model.NationalId);
            if (isUserExist)
            {
                return new ServiceMessage()
                {
                    ErrorId = -1 ,
                    Result=null , 
                    ErrorTitle = SystemMessages.UserExist
                };
            }
            var res = await _mediator.Send(new RegisterUserRequest()
            {
                dto = model
            });
            return res;
        }

        [Route("SendOtp")]
        [HttpPost]
        public async Task<ActionResult<ServiceMessage>> SendOtp(SendOtpDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return new ServiceMessage()
                {
                    ErrorId = -1,
                    Result = errors,
                    ErrorTitle = "داده‌های ورودی معتبر نیستند"
                };
            }
            var user = await _user.FirstOrDefault(where: u => u.MobileNumber == model.MobileNumber);
            if(user == null)
            {
                return new ServiceMessage()
                {
                    ErrorId = -1,
                    Result = null,
                    ErrorTitle = SystemMessages.UserNotFound
                };
            }
            string code = 5.GenerateCode();
            user.ActiveCode = code;
            await _user.Upsert(user);
            return new ServiceMessage()
            {
                ErrorId = 0,
                Result = SystemMessages.ActiveCodeSms,
                ErrorTitle = null
            };

        }

        [Route("Ok")]
        [HttpGet]
        public async Task<ActionResult<ServiceMessage>> OK()
        {
            return new ServiceMessage()
            {
                ErrorId = -1,
                Result = null,
                ErrorTitle = SystemMessages.UserExist
            };
        }

    }
}
