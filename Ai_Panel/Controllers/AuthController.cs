using System.Reflection;
using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.User;
using Ai_Panel.Application.Features.User.Request.Command;
using Ai_Panel.Application.Tools;
using Ai_Panel.Domain;
using Ai_Panel.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersianAssistant.Extensions;
using PersianAssistant.Models;

namespace Ai_Panel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUser _user;
        private readonly IGenericRepository<Role> _role;
        private readonly IMediator _mediator;
        private readonly IJwtTokenGenerator _jwt;
        private readonly IGenericRepository<UserSession> _userSession;
        private readonly IGenericRepository<UserRole> _userRole;
        private readonly Sms _sms;
        public AuthController(
            IUser user,
            IMediator mediator,
            IJwtTokenGenerator jwt,
            IGenericRepository<UserSession> userSession,
            IGenericRepository<Role> role,
            IGenericRepository<UserRole> userRole,
            Sms sms
            )
        {
            _user = user;
            _mediator = mediator;
            _jwt = jwt;
            _userSession = userSession;
            _role = role;
            _userRole = userRole;
            _sms = sms;
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
                    ErrorId = -1,
                    Result = null,
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
            if (user == null)
            {
                var role = await _role.FirstOrDefault(where:r=>r.RoleTitle == "User");
                User newUser = new User()
                {
                    MobileNumber = model.MobileNumber,
                    ActiveCode = 5.GenerateCode(),
                    DateTime = DateTime.UtcNow.AddHours(3.5),
                    UpdateDateTime = DateTime.UtcNow.AddHours(3.5),
                    UserType = UserTypeEnum.USER,
                };
                var addedUser =  await _user.Upsert(newUser);

                UserRole userRole = new UserRole()
                {
                    RoleId = role.RoleId,
                    UserId = addedUser.UserId
                };
                await _userRole.Add(userRole);
                await _sms.SendSms(addedUser.MobileNumber, addedUser.ActiveCode, "");
                return new ServiceMessage()
                {
                    ErrorId = 0,
                    Result = SystemMessages.ActiveCodeSms,
                    ErrorTitle = null
                };
            }
            string code = 5.GenerateCode();
            user.ActiveCode = code;
            await _user.Upsert(user);
            await _sms.SendSms(user.MobileNumber, code, "");
            return new ServiceMessage()
            {
                ErrorId = 0,
                Result = code,
                ErrorTitle = null
            };
        }

        [Route("VerifyOtp")]
        [HttpPost]
        public async Task<ActionResult<ServiceMessage>> VerifyOtp(VerifyOtpDto model)
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
            if (user == null)
            {
                return new ServiceMessage()
                {
                    ErrorId = -1,
                    Result = null,
                    ErrorTitle = SystemMessages.UserNotFound
                };
            }

            if (user.ActiveCode != model.Code)
            {
                return new ServiceMessage()
                {
                    ErrorId = -1,
                    Result = null,
                    ErrorTitle = SystemMessages.ActiveCodeIncorrect
                };
            }


            string token = _jwt.GenerateToken(user.UserId);

            UserSession session = new UserSession()
            {
                Token = token,
                DateTime = DateTime.UtcNow.AddHours(3.5),
                UserId = user.UserId,
                IsLogout = false,
                DateLogout = null
            };
            await _userSession.Add(session);

            UserDetailDto User = new UserDetailDto()
            {
                Id = user.UserId,
                MobileNumber = user.MobileNumber,
                Avatar = user.Avatar,
                DateTime = user.DateTime,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsPremiumAccount = user.IsPremiumAccount,
                Gender = user.Gender,
                NationalId = user.NationalId,
                UserType = user.UserType,
                Token = token
            };
            return new ServiceMessage()
            {
                ErrorId = 0,
                ErrorTitle = null,
                Result = User
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
