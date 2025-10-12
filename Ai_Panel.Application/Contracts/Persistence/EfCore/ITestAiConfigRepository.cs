using Ai_Panel.Domain;

namespace Ai_Panel.Application.Contracts.Persistence.EfCore;

public interface ITestAiConfigRepository : IGenericRepository<TestAiConfig>
{
    public Task<List<int>> GetAdminsId();
}