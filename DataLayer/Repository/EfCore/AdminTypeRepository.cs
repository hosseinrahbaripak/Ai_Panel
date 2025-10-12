using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;

namespace Ai_Panel.Persistence.Repository.EfCore;
public class AdminTypeRepository : GenericRepository<AdminType>, IAdminTypeRepository
{
	public AdminTypeRepository(LiveBookContext context) : base(context)
	{
	}
}
