using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Application.DTOs.Aiservice;
using MediatR;

namespace Ai_Panel.Application.Features.AiServices.Request.Command
{
    public class UpserAiServiceRequest:IRequest<Unit>
    {
        public UpsertAiServiceDto AiService {  get; set; }
    }
}
