using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Domain;
using Live_Book.Persistence.Configurations;

namespace Live_Book.Persistence.Repository.EfCore;
public class AdminTypeRepository : GenericRepository<AdminType>, IAdminTypeRepository
{
	public AdminTypeRepository(LiveBookContext context) : base(context)
	{
	}
}
