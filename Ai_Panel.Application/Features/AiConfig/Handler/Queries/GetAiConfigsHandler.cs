using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.AiConfig;
using Ai_Panel.Application.Features.AiConfig.Request.Queries;
using MediatR;

namespace Ai_Panel.Application.Features.AiConfig.Handler.Queries
{
    public class GetAiConfigsHandler : IRequestHandler<GetAiConfigsRequest, List<GetAiConfigsDto>>
    {
        private readonly IGenericRepository<Domain.AiConfigGroup> _aiConfigGroup;
        public GetAiConfigsHandler(IGenericRepository<Domain.AiConfigGroup> aiConfigGroup)
        {
            _aiConfigGroup = aiConfigGroup;
        }

        public async Task<List<GetAiConfigsDto>> Handle(GetAiConfigsRequest request, CancellationToken cancellationToken)
        {
            var res = await _aiConfigGroup.GetAll(where: conf => !conf.IsDelete,null,"AiConfig");

            List<GetAiConfigsDto> AiConfigs = new List<GetAiConfigsDto>();
            foreach(var config in res)
            {
                AiConfigs.Add(new GetAiConfigsDto() { Id = config.Id , Title = config.AiConfig.FirstOrDefault().Title});
            }
            return AiConfigs;
        }
    }
}
