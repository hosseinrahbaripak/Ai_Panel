using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Domain;
using Live_Book.Persistence.Configurations;

namespace Live_Book.Persistence.Repository.EfCore;

public class UserAiChatLogsRepository : GenericRepository<UserAiChatLog>, IUserAiChatLogsRepository
{
	private readonly LiveBookContext _context;
	public UserAiChatLogsRepository(LiveBookContext context) : base(context)
    {
		_context = context;
    }
	public async Task<List<int>> GetUsersId()
	{		
		var dbSet = _context.Set<UserAiChatLog>();
		IQueryable<UserAiChatLog> query = dbSet;
		return query.Select(x => x.UserId).Distinct().ToList();
	}
}