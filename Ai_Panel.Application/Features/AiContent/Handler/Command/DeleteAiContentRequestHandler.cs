using System.Linq.Expressions;
using System.Transactions;
using Ai_Panel.Application.Constants;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.Features.AiContent.Request.Command;
using MediatR;
using PersianAssistant.Extensions;
using PersianAssistant.Models;

namespace Ai_Panel.Application.Features.AiContent.Handler.Command;
public class DeleteAiContentRequestHandler : IRequestHandler<DeleteAiContentRequest, ServiceMessage>
{

	public async Task<ServiceMessage> Handle(DeleteAiContentRequest request, CancellationToken cancellationToken)
	{
		//try
		//{
		//	var models = new List<Domain.AiContent>();
		//	if (request.Id > 0)
		//	{
		//		models.Add(await _aiContentRepository.Get(request.Id));
		//	}
		//	else if(request.AiConfigId > 0)
		//	{
		//		Expression<Func<Domain.AiContent, bool>> where = x => !x.IsDelete && x.AiConfigId == request.AiConfigId; ;
		//		if (request.Bulk)
		//			models = await _aiContentRepository.GetAll(where);
		//		else
		//			models.Add(await _aiContentRepository.LastOrDefault(where, x => x.OrderBy(o => o.Id)));
		//	}
		//	if (models == null)
		//		return ResponseManager.DataError(SystemMessages.AiContentNotFound);
		//	foreach(var model in models)
		//	{
		//		if (model == null)
		//			continue;
		//		using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
		//		{
		//			model.IsDelete = true;
		//			var bookPartModel = await _bookPart.GetPartById(model.PartId);
		//			if (bookPartModel != null)
		//			{
		//				bookPartModel.AiContentId = null;
		//				await _bookPart.UpdatePart(bookPartModel);
		//			}
		//			await _aiContentRepository.Update(model);
		//			scope.Complete();
		//		}	
		//	}
			return ResponseManager.FillObject(SystemMessages.DeleteSuccess);
	}
}

