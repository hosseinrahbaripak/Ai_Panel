using System.Linq.Expressions;
using AutoMapper;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.DTOs.AiContent;
using Ai_Panel.Application.Features.AiContent.Request.Queries;
using Ai_Panel.Domain;
using MediatR;

namespace Ai_Panel.Application.Features.AiContent.Handler.Queries;

public class GetAiContentRequestHandler : IRequestHandler<GetAiContentRequest, AiContentUpsertDto?>
{
    private readonly IMapper _mapper;


    public GetAiContentRequestHandler(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<AiContentUpsertDto?> Handle(GetAiContentRequest request, CancellationToken cancellationToken)
	{
        return null;
        //Expression<Func<Domain.AiContent, bool>> where;
        //if (request.Id > 0)
        //    where = x => !x.IsDelete && x.Id == request.Id;
        //else if (request.AiConfigId > 0)
        //    where = x => !x.IsDelete && x.AiConfigId == request.AiConfigId;
        //else
        //    return null;
        //var aiContent = await _aiContentRepository.LastOrDefault(where, x => x.OrderBy(o => o.Id),
        //        "Part,Book,Book.BookCategory,AiConfig,");
        //if (aiContent != null)
        //    return _mapper.Map<AiContentUpsertDto>(aiContent);
        //return null;
    }
}