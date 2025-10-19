using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;

namespace Ai_Panel.Persistence.Repository.EfCore;
public class AiPlatformRepository : GenericRepository<AiPlatform>, IAiPlatformRepository {
    public AiPlatformRepository(AiPanelContext context) : base(context)
    {
    }
}


