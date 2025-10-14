using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.Features.AiContent.Request.Command;
using Ai_Panel.Application.Features.User.Request.Command;
using Ai_Panel.Application.Tools;
using MediatR;
using PersianAssistant.Extensions;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.User.Handler.Command
{
    public class UpsertUserHandler : IRequestHandler<UpsertUserRequest, ServiceMessage>
    {
        private readonly IUser _user;
        public UpsertUserHandler(IUser user)
        {
            _user = user;
        }
        public async Task<ServiceMessage> Handle(UpsertUserRequest request, CancellationToken cancellationToken)
        {
            bool userExist = await _user.Any(where:u => u.MobileNumber == request.User.MobileNumber || u.Email == request.User.Email);
            if (userExist)
            {
                return new ServiceMessage() {
                    ErrorId = -1,
                    ErrorTitle = SystemMessages.NoNewUser,
                    Result = null
                };
            }
            var (key, hashedPassword) = request.User.Password.GeneratePass();

            var user = new Domain.User()
            {
                Email = request.User.Email,
                MobileNumber = request.User.MobileNumber.ToIranMobileNumber(),
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                DateTime = DateTime.UtcNow,
                IsDelete = false,
                UpdateDateTime = DateTime.UtcNow,
                Password = hashedPassword,
                PassKey = key,
            };
            await _user.Upsert(user);
            return new ServiceMessage()
            {
                ErrorId = 0,
                ErrorTitle = null,
                Result = SystemMessages.Success
            };
        }
    }
}
