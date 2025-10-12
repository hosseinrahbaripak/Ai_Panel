using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Domain;
using Live_Book.Persistence.Configurations;

namespace Live_Book.Persistence.Repository.EfCore;
public class AiPlatformRepository : GenericRepository<AiPlatform>, IAiPlatformRepository {
    public AiPlatformRepository(LiveBookContext context) : base(context)
    {
    }
}


