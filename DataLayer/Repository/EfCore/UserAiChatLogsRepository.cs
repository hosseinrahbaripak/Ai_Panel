using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;

namespace Ai_Panel.Persistence.Repository.EfCore;

public class UserAiChatLogsRepository : GenericRepository<UserAiChatLog>, IUserAiChatLogsRepository
{
	private readonly AiPanelContext _context;
	public UserAiChatLogsRepository(AiPanelContext context) : base(context)
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