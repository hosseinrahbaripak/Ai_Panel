using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;

namespace Ai_Panel.Persistence.Repository.EfCore;

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