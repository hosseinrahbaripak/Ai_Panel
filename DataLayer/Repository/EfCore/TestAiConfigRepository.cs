using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Domain;
using Live_Book.Persistence.Configurations;

namespace Live_Book.Persistence.Repository.EfCore;

public class TestAiConfigRepository : GenericRepository<TestAiConfig>, ITestAiConfigRepository
{
	private readonly LiveBookContext _context;
	public TestAiConfigRepository(LiveBookContext context) : base(context)
    {
		_context = context;
	}
	public async Task<List<int>> GetAdminsId()
	{
		var dbSet = _context.Set<TestAiConfig>();
		IQueryable<TestAiConfig> query = dbSet;
		return query.Select(x => x.AdminId).Distinct().ToList();
	}
}