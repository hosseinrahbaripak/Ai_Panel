using MediatR;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.User.Request.Command
{
    public class UpsertUserRequest:IRequest<ServiceMessage>
    {
        public Domain.User User { get; set; }
    }
}
