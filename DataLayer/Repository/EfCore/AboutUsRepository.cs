using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Ai_Panel.Persistence.Repository.EfCore
{
	public class AboutUsRepository : IAboutUs
    {
        private readonly LiveBookContext _context;
        public AboutUsRepository(LiveBookContext context)
        {
            _context = context;
        }
        public async Task<List<AboutUs>> GetAboutUs()
        {
            return await _context.AboutUs.ToListAsync();
        }

        public async Task AddAboutUs(AboutUs aboutUs)
        {
            await _context.AddAsync(aboutUs);
            await _context.SaveChangesAsync();
        }


        public async Task EditAboutUs(AboutUs aboutUs)
        {
            _context.Update(aboutUs);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExistAboutUs()
        {
            return await _context.AboutUs.AnyAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<AboutUs> FindAboutUs(int id)
        {
            return await _context.AboutUs.FindAsync(id);
        }
    }
}
