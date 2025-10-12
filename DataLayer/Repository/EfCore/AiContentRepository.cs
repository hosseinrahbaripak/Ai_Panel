using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Domain;
using Live_Book.Persistence.Configurations;

namespace Live_Book.Persistence.Repository.EfCore;

public class AiContentRepository : GenericRepository<AiContent>, IAiContentRepository
{
    public AiContentRepository(LiveBookContext context) : base(context)
    {
    }
}