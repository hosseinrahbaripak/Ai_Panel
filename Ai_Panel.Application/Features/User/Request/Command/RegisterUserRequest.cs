using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Application.DTOs.User;
using MediatR;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.User.Request.Command
{
    public class RegisterUserRequest : IRequest<ServiceMessage>
    {
        public RegisterUserDto dto {get;set;}
    }
}
