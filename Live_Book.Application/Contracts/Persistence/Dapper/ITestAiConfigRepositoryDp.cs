using Live_Book.Application.DTOs.TestAiConfig;

namespace Live_Book.Application.Contracts.Persistence.Dapper;
public interface ITestAiConfigRepositoryDp
{
	Task<List<TestAiConfigLogDto>> GetTestAiConfigLogs(string where, string orderBy, object parameters);
}

