using AutoMapper;
using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Application.DTOs.AiConfig;
using Live_Book.Application.Features.AiConfig.Request.Queries;
using MediatR;

namespace Live_Book.Application.Features.AiConfig.Handler.Queries;

public class GetAiConfigRequestHandler : IRequestHandler<GetAiConfigRequest, UpsertAiConfigDto?>
{
	private readonly IMapper _mapper;
	private readonly IAiConfigRepository _aiConfigRepository;

	public GetAiConfigRequestHandler(IMapper mapper, IAiConfigRepository aiConfigRepository)
	{
		_mapper = mapper;
		_aiConfigRepository = aiConfigRepository;
	}
	public async Task<UpsertAiConfigDto?> Handle(GetAiConfigRequest request, CancellationToken cancellationToken)
	{
		var aiConfig = await _aiConfigRepository.LastOrDefault(x => !x.IsDelete && x.Id == request.Id, x => x.OrderBy(o => o.Id), "AiModel,AiModel.Parent");
		if (aiConfig != null)
			return _mapper.Map<UpsertAiConfigDto>(aiConfig);
		return null;
	}
}