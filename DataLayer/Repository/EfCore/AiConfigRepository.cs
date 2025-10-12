using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Application.Tools;
using Live_Book.Domain;
using Live_Book.Persistence.Configurations;

namespace Live_Book.Persistence.Repository.EfCore;

public class AiConfigRepository : GenericRepository<AiConfig>, IAiConfigRepository
{
	public AiConfigRepository(LiveBookContext context) : base(context)
	{
	}

	public async Task<string?> GenerateVersion(int adminId)
	{
		var lastAdminAiConfig = await LastOrDefault(x => x.CreateBy == adminId, x => x.OrderBy(o => o.Id));
		if (lastAdminAiConfig == null)
		{
			return "1.0.0";
		}
		else
		{
			string? newVersion = Utility.GenerateVersion(lastAdminAiConfig.Version);
			return newVersion;
		}
	}
}