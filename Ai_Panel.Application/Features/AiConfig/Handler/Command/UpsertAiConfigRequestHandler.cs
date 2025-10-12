using AutoMapper;
using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.Features.AiConfig.Request.Command;
using MediatR;
using PersianAssistant.Extensions;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.AiConfig.Handler.Command;

public class UpsertAiConfigRequestHandler : IRequestHandler<UpsertAiConfigRequest, ServiceMessage>
{
	private readonly IMapper _mapper;
	private readonly IAiConfigRepository _aiConfigRepository;

	public UpsertAiConfigRequestHandler(IMapper mapper, IAiConfigRepository aiConfigRepository)
	{
		_mapper = mapper;
		_aiConfigRepository = aiConfigRepository;
	}
	public async Task<ServiceMessage> Handle(UpsertAiConfigRequest request, CancellationToken cancellationToken)
	{
		Domain.AiConfig model;
		if (request.UpsertAiConfig.Id == 0)
		{
			var version = await _aiConfigRepository.GenerateVersion(request.UpsertAiConfig.CreateBy);
			if (string.IsNullOrEmpty(version))
				return ResponseManager.DataError(SystemMessages.ErrorInCreateVersion);
			model = _mapper.Map<Domain.AiConfig>(request.UpsertAiConfig);
			model.Version = version;
			//if (await _aiConfigRepository.AnyAsync(x => x.Version == model.Version && x.CreateBy != model.CreateBy && !x.IsDelete))
			//	return ResponseManager.DataError("شماره نسخه موجود می باشد !");
			await _aiConfigRepository.Add(model);	
		}
		else
		{
			model = await _aiConfigRepository.Get(request.UpsertAiConfig.Id);
			if (model == null)
				return ResponseManager.DataError(SystemMessages.AiConfigNotFound);
			model = _mapper.Map(request.UpsertAiConfig, model);
			await _aiConfigRepository.Update(model);
		}
		return ResponseManager.FillObject(model.Id);
	}
}