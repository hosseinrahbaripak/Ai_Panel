using Live_Book.Domain;

namespace Live_Book.Application.Contracts.Persistence.EfCore;

public interface IAiConfigRepository : IGenericRepository<AiConfig>
{
	Task<string?> GenerateVersion(int adminId);
}