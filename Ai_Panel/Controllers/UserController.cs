using System.IdentityModel.Tokens.Jwt;
using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersianAssistant.Models;

namespace Ai_Panel.Controllers
{
    [Authorize(AuthenticationSchemes = "JwtBearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _user;
        public UserController(IUser user) {
            _user = user;
        }


        [Route("Get")]
        [HttpGet]
        public async Task<ActionResult<ServiceMessage>> GetUser()
        {
            var userId = User.FindFirst("uid")?.Value;
            if(userId == null)
            {
                return new ServiceMessage()
                {
                    ErrorId=-1 , 
                    ErrorTitle=SystemMessages.UserNotFound , 
                    Result = null
                };
            }
            var user = await _user.FirstOrDefault(where: u => u.UserId == Int64.Parse(userId));
            UserDetailDto UserDto = new UserDetailDto()
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
            };
            return new ServiceMessage()
            {
                Result = UserDto , 
                ErrorId = 0 ,
                ErrorTitle = null
            };

        }
    }
}
