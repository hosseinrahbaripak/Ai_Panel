using Ai_Panel.Application.DTOs.TestAiConfig;

namespace Ai_Panel.Application.Contracts.Persistence.Dapper;
public interface ITestAiConfigRepositoryDp
{
	Task<List<TestAiConfigLogDto>> GetTestAiConfigLogs(string where, string orderBy, object parameters);
}

