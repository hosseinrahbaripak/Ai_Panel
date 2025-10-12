using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;

namespace Ai_Panel.Persistence.Repository.EfCore
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
