using Ai_Panel.Domain;

namespace Ai_Panel.Application.Contracts.Persistence.EfCore;

public interface IUserAiChatLogsRepository : IGenericRepository<UserAiChatLog>
{
	Task<List<int>> GetUsersId();
}