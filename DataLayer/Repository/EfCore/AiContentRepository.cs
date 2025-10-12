using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;

namespace Ai_Panel.Persistence.Repository.EfCore;

public class AiContentRepository : GenericRepository<AiContent>, IAiContentRepository
{
    public AiContentRepository(LiveBookContext context) : base(context)
    {
    }
}