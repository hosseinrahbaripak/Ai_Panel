using Live_Book.Domain;

namespace Live_Book.Application.Contracts.Persistence.EfCore;

public interface IUserAiChatLogsRepository : IGenericRepository<UserAiChatLog>
{
	Task<List<int>> GetUsersId();
}