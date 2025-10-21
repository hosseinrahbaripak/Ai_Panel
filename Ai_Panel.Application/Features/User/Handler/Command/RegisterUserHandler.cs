using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.Features.User.Request.Command;
using MediatR;
using PersianAssistant.Extensions;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.User.Handler.Command
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, ServiceMessage>
    {
        private readonly IUser _user;
        private readonly IErrorLog _errorLog;
        public RegisterUserHandler(IUser user , IErrorLog errorLog)
        {
            _user = user;
            _errorLog = errorLog;
        }
        public async Task<ServiceMessage> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            string Code = 5.GenerateCode();
            Domain.User user = new Domain.User()
            {
                FirstName = request.dto.FirstName,
                LastName = request.dto.LastName,
                NationalId = request.dto.NationalId,
                MobileNumber = request.dto.MobileNumber,
                UserType = Domain.Enum.UserTypeEnum.USER , 
                DateTime = DateTime.UtcNow.AddHours(3.5),
                UpdateDateTime = DateTime.UtcNow.AddHours(3.5),
                ActiveCode = Code,
            };
            try
            {
                await _user.Upsert(user);
                return new ServiceMessage()
                {
                    ErrorId = 0,
                    Result = Code,
                    ErrorTitle = null
                };
            }
            catch (Exception ex) {
                await _errorLog.Add(ex.Message, ex.InnerException?.Message ?? "", "Register new User");
                return new ServiceMessage()
                {
                    ErrorId = -1,
                    Result = null,
                    ErrorTitle = SystemMessages.ErrorAddUserToDb,
                };


            }

            

            
        }
    }
}
