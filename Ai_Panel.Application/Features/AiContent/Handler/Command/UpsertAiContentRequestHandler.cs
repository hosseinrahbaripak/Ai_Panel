using System.Transactions;
using AutoMapper;
using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.Features.AiContent.Request.Command;
using Ai_Panel.Domain;
using MediatR;
using PersianAssistant.Extensions;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.AiContent.Handler.Command;

public class UpsertAiContentRequestHandler : IRequestHandler<UpsertAiContentRequest,ServiceMessage>
{
    private readonly IMapper _mapper;


    public UpsertAiContentRequestHandler(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task<ServiceMessage> Handle(UpsertAiContentRequest request, CancellationToken cancellationToken)
    {
        return ResponseManager.DataError(SystemMessages.ServerError);

  //      Domain.AiContent model;
  //      using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
  //      {
  //          try
  //          {
		//		if (request.Model.Id == 0)
		//		{
		//			model = _mapper.Map<Domain.AiContent>(request.Model);
		//			await _aiContentRepository.Add(model);
		//		}
		//		else
		//		{
		//			model = await _aiContentRepository.Get(request.Model.Id);
		//			if (model == null)
		//				return ResponseManager.DataError(SystemMessages.AiContentNotFound);
		//			#region removing ai content of previous book part
		//			bookPartModel = await _bookPart.GetPartById(model.PartId);
		//			if (bookPartModel != null)
		//			{
		//				bookPartModel.AiContentId = null;
		//				await _bookPart.UpdatePart(bookPartModel);
		//			}
		//			#endregion
		//			model = _mapper.Map(request.Model, model);
		//			await _aiContentRepository.Update(model);
		//		}
		//		#region adding ai content of new book part
		//		bookPartModel = await _bookPart.GetPartById(model.PartId);
		//		if (bookPartModel == null)
		//			return ResponseManager.DataError(SystemMessages.BookPartNotFound);
		//		bookPartModel.AiContentId = model.Id;
		//		await _bookPart.UpdatePart(bookPartModel);
		//		#endregion
		//		scope.Complete();
		//		return ResponseManager.FillObject(model.Id);
		//	}
  //          catch {
		//		return ResponseManager.DataError(SystemMessages.ServerError);
		//	}
			
		//}
    }
}