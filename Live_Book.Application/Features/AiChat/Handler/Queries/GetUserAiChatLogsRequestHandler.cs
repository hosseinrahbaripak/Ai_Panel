using Live_Book.Application.Contracts.Persistence.Dapper;
using Live_Book.Application.DTOs.AiChat;
using Live_Book.Application.Features.AiChat.Request.Queries;
using MediatR;

namespace Live_Book.Application.Features.AiChat.Handler.Queries;
public class GetUserAiChatLogsRequestHandler : IRequestHandler<GetUserAiChatLogsRequest, AiChatDashboardDto>
{
	private readonly IUserAiChatLogsRepositoryDp _userAiChatLogsRepositoryDp;
	public GetUserAiChatLogsRequestHandler(IUserAiChatLogsRepositoryDp userAiChatLogsRepositoryDp)
	{
		_userAiChatLogsRepositoryDp = userAiChatLogsRepositoryDp;
	}
	public async Task<AiChatDashboardDto> Handle(GetUserAiChatLogsRequest request, CancellationToken cancellationToken)
	{
		UserAiChatLogsFilter filter = request.ModelFilter;
		string where = "";
		string orderBy = "";
		if (filter?.BookId > 0)
		{
			where += "AND uacl.BookId = @BookId ";
		}
		if (filter?.PartId > 0)
		{
			where += "AND uacl.PartId = @PartId ";
		}
		if (filter?.UserId > 0)
		{
			where += "AND uacl.UserId = @UserId ";
		}
		if (!String.IsNullOrEmpty(filter?.OrderBy))
		{

			var orderList = new List<string>();
			var orderByFilter = filter.OrderBy.Split(",");
			foreach (var item in orderByFilter)
			{
				string order = "";
				if (item.Contains("Name"))
					order += "u.Name ";

				if (item.Contains("Book"))
					order += "b.Name ";

				if (item.Contains("Part"))
					order += "bp.PartName ";

				if (item.Contains("Date"))
					order += "uacl.DateTime ";

				if (item.Contains("Cost"))
					order += "(uacl.SummarizationCost + uacl.RequestCost + uacl.EmbeddingCost) ";

				if (item.Contains("AiCouldResponse"))
					order += "uacl.AiCouldResponse ";

				if (item.StartsWith("-"))
					order += " Desc ";

				if (!string.IsNullOrEmpty(order))
					orderList.Add(order);
			}
			orderBy += " ORDER BY " + String.Join(" , ", orderList);
		}
		
		var parameters = new
		{
			BookId = filter?.BookId,
			PartId = filter?.PartId,
			UserId = filter?.UserId
		};
		var model = new AiChatDashboardDto()
		{
			AiChatLogs = await _userAiChatLogsRepositoryDp.GetAiChatlogs(where, orderBy, parameters),
			OrderBy = filter!= null ? String.Join(",", filter.OrderBy) : ""
		};
		return model;
	}
}
