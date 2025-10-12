using Live_Book.Domain;

namespace Live_Book.Application.Contracts.Persistence.EfCore;

public interface ITestAiConfigRepository : IGenericRepository<TestAiConfig>
{
    public Task<List<int>> GetAdminsId();
}