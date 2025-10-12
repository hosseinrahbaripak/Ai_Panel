using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;

namespace Ai_Panel.Persistence.Repository.EfCore;

public class AiModelRepository : GenericRepository<AiModel>,IAiModelRepository
{
    public AiModelRepository(LiveBookContext context) : base(context)
    {
    }
}