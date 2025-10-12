using Dapper;
using Live_Book.Application.Contracts.Persistence.Dapper;
using Live_Book.Application.DTOs.TestAiConfig;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Live_Book.Persistence.Repository.Dapper;
public class TestAiConfigRepositoryDp : ITestAiConfigRepositoryDp
{
	private readonly IDbConnection _db;
	public TestAiConfigRepositoryDp(IConfiguration configuration)
	{
		_db = new SqlConnection(configuration.GetConnectionString("LiveBookConnection"));
	}
	public async Task<List<TestAiConfigLogDto>> GetTestAiConfigLogs(string where, string orderBy, object parameters)
	{
		var query = @"SELECT " +
			"al.UserName AS Name, b.Name AS Book, bp.PartName AS Part, " +
			"tac.Message AS UserMessage, tac.AiResponse, " +
			"tac.DateTime AS Date, " +
			"(tac.SummarizationCost + tac.RequestCost + tac.EmbeddingCost) AS Cost, " +
			"tac.MaxTokens, tac.Prompt, tac.Temperature, tac.TopP, " +
			"tac.PresencePenalty, tac.FrequencyPenalty, tac.Stop AS StopJson, " +
			"am.title As AiModel , am2.Title As Ai " +
			"FROM TestAiConfigs tac " +
			"INNER JOIN AdminLogins al ON tac.AdminId = al.LoginId " +
			"LEFT OUTER JOIN Books b ON tac.BookId = b.Id AND b.IsDelete = 0" +
			"LEFT OUTER JOIN BookParts bp ON tac.PartId = bp.PartId AND b.IsDelete = 0" +
			"LEFT OUTER JOIN AiModels am on tac.AiModelId = am.Id  " +
			"LEFT OUTER JOIN AiModels am2 on am.ParentId = am2.Id " +
			"WHERE al.IsDelete = 0 " +
			where +
			orderBy;
		var model = await _db.QueryAsync<TestAiConfigLogDto>(query, parameters);
		return model.ToList();
	}
}

