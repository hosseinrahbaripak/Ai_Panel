using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Application.DTOs.Contract;
using MediatR;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.Contract.Request.Command
{
    public class UpsertContractCommand:IRequest<ServiceMessage>
    {
        public UpserContractDto Contract { get; set; }
    }
}
