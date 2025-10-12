using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Domain;
using Live_Book.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Live_Book.Persistence.Repository.EfCore
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
