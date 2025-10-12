using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Domain;
using Live_Book.Persistence.Configurations;

namespace Live_Book.Persistence.Repository.EfCore;

public class AiModelRepository : GenericRepository<AiModel>,IAiModelRepository
{
    public AiModelRepository(LiveBookContext context) : base(context)
    {
    }
}