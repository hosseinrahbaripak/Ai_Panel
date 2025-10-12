using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Domain;
using Live_Book.Persistence.Configurations;

namespace Live_Book.Persistence.Repository.EfCore
{
	public class RoleInPagesService : GenericRepository<RolesInPages>, IRoleInPages
    {
        private readonly LiveBookContext _context;

        public RoleInPagesService(LiveBookContext context) : base(context)
        {
            _context = context;
        }
    }
}
