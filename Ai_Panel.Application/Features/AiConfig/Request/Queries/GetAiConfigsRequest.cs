using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Application.DTOs.AiConfig;
using MediatR;

namespace Ai_Panel.Application.Features.AiConfig.Request.Queries
{
    public class GetAiConfigsRequest:IRequest<List<GetAiConfigsDto>>
    {
        public int? Take { get; set; } = 10;
        public int? Skip { get; set; } = 1;
    }
}
