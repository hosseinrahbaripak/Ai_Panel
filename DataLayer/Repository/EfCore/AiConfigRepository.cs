using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Application.Tools;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;

namespace Ai_Panel.Persistence.Repository.EfCore;

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