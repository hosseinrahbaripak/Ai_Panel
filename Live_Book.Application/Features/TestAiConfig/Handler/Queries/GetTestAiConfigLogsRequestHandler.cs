using System.Diagnostics.Eventing.Reader;
using Live_Book.Application.Contracts.Persistence.Dapper;
using Live_Book.Application.DTOs.TestAiConfig;
using Live_Book.Application.Features.TestAiConfig.Request.Queries;
using MediatR;

namespace Live_Book.Application.Features.TestAiConfig.Handler.Queries;
public class GetTestAiConfigLogsRequestHandler(ITestAiConfigRepositoryDp TACRepository) : IRequestHandler<GetTestAiConfigLogsRequest, TestAiConfigDashboardDto>
{
	public async Task<TestAiConfigDashboardDto> Handle(GetTestAiConfigLogsRequest request, CancellationToken cancellationToken)
	{
		TestAiConfigFilter filter = request.ModelFilter;
		string where = "";
		string orderBy = "";

		// Filters
		if (filter?.BookId > 0)
			where += "AND tac.BookId = @BookId ";

		if (filter?.PartId > 0)
			where += "AND tac.PartId = @PartId ";

		if (filter?.UserId > 0)
			where += "AND tac.AdminId = @UserId ";

		if (filter?.AiModelId > 0)
			where += "AND am.Id = @AiModelId ";

		else if (filter?.AiId > 0)
			where += "AND am2.Id = @AiId ";

		// Orders
		if (!string.IsNullOrEmpty(filter?.OrderBy))
		{
			var orderList = new List<string>();
			var orderByFilter = filter.OrderBy.Split(",");

			foreach (var item in orderByFilter)
			{
				string order = "";
				if (item.Contains("UserName"))
					order += "al.UserName ";

				if (item.Contains("Book"))
					order += "b.Name ";

				if (item.Contains("Part"))
					order += "bp.PartName ";

				if (item.Contains("Date"))
					order += "tac.DateTime ";

				if (item.Contains("MaxTokens"))
					order += "tac.MaxTokens ";

				if (item.Contains("Temperature"))
					order += "tac.Temperature ";

				if (item.Contains("TopP"))
					order += "tac.TopP ";

				if (item.Contains("PresencePenalty"))
					order += "tac.PresencePenalty ";

				if (item.Contains("FrequencyPenalty"))
					order += "tac.FrequencyPenalty ";

				if (item.Contains("Ai"))
					order += "am2.title ";

				if (item.Contains("Model"))
					order += "am.title ";

				if (item.Contains("Cost"))
					order += "(tac.SummarizationCost + tac.RequestCost + tac.EmbeddingCost) ";

				if (item.Contains("AiCouldResponse"))
					order += "tac.AiCouldResponse ";

				if (item.StartsWith("-"))
					order += " Desc ";

				if (!string.IsNullOrEmpty(order))
					orderList.Add(order);
			}
			orderBy += " ORDER BY " + string.Join(" , ", orderList);
		}
		else
			orderBy += " ORDER BY tac.DateTime Desc ";

			var parameters = new
			{
				filter?.BookId,
				filter?.PartId,
				filter?.UserId,
				filter?.AiModelId,
				filter?.AiId,
			};
		
		var model = new TestAiConfigDashboardDto()
		{
			TestAiConfigLogs = await TACRepository.GetTestAiConfigLogs(where, orderBy, parameters),
			OrderBy = filter != null ? string.Join(",", filter.OrderBy) : "-Date"
		};
		return model;
	}
}

