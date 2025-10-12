using Live_Book.Application.Contracts.Persistence.EfCore;
using Live_Book.Domain;
using Live_Book.Domain.Enum;
using Live_Book.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Live_Book.Persistence.Repository.EfCore
{
    public class AdminManageRepository : IAdminManage
    {
        private readonly LiveBookContext _context;
        public AdminManageRepository(LiveBookContext context)
        {
            _context = context;
        }
        public async Task<List<AdminLogin>> GetAll(Expression<Func<AdminLogin, bool>> where = null, int skip = 0, int take = int.MaxValue)
        {
            if (where != null)
            {
                return await _context.AdminLogins.Include(u => u.Role).OrderBy(x => x.LoginID)
                    .Where(x => x.IsDelete == false).OrderByDescending(x => x.LoginID).Where(where).Skip(skip).Take(take).ToListAsync();
            }
            return await _context.AdminLogins.Include(u => u.Role).OrderBy(x => x.LoginID)
                .Where(x => x.IsDelete == false).OrderByDescending(x => x.LoginID).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<int> GetCount(Expression<Func<AdminLogin, bool>> where = null)
        {
            if (where != null)
            {
                return await _context.AdminLogins.Where(x => x.IsDelete == false).Where(where).CountAsync();
            }
            return await _context.AdminLogins.Where(x => x.IsDelete == false).CountAsync();
        }
        public async Task<AdminLogin> FirstOrDefault(Expression<Func<AdminLogin, bool>> @where = null)
        {
            if (where != null)
            {
                return await _context.AdminLogins.Include(x => x.Role)
                    .Where(x => x.IsDelete == false).FirstOrDefaultAsync(where);
            }
            return null;
        }
        public async Task<AdminLogin> Add(AdminLogin admin)
        {
            await _context.AdminLogins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
        public async Task Update(AdminLogin admin)
        {
            _context.AdminLogins.Update(admin);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAdminLogin(AdminLogin admin)
        {
            admin.IsDelete = true;
            await Update(admin);
        }
        public async Task DeleteAdminLogin(int id)
        {
            var admin = await FirstOrDefault(x => x.LoginID == id);
            if (admin != null)
            {
                admin.IsDelete = true;
                await Update(admin);
            }
        }

        public async Task<int?> GetAdminProfileId(int adminId)
        {
            //int adminTypeProfileId = 0;
            //var admin = await FirstOrDefault(x => x.LoginID == adminId);

            //if (admin.Role.AdminTypeId == (int)AdminTypeIdEnum.GeneralAdmin)
            //    adminTypeProfileId = admin.LoginID;
            //if (admin.Role.AdminTypeId == (int)AdminTypeIdEnum.ProjectManager)
            //{
            //    var project = await _context.ProjectProfiles.FirstOrDefaultAsync(x => !x.IsDelete && x.AdminId == adminId);
            //    if (project == null)
            //        return null;
            //    adminTypeProfileId = project.Id;
            //}
            //if (admin.Role.AdminTypeId == (int)AdminTypeIdEnum.Supervisor)
            //{
            //    var supervisor = await _context.SupervisorProfiles.FirstOrDefaultAsync(x => !x.IsDelete && x.AdminId == adminId);
            //    if (supervisor == null)
            //        return null;
            //    adminTypeProfileId = supervisor.Id;
            //}
            //if (admin.Role.AdminTypeId == (int)AdminTypeIdEnum.Advisor || admin.Role.AdminTypeId == (int)AdminTypeIdEnum.ParentAdvisor)
            //{
            //    var advisor = await _context.AdvisorProfiles.FirstOrDefaultAsync(x => !x.IsDelete && x.AdminId == adminId);
            //    if (advisor == null)
            //        return null;
            //    adminTypeProfileId = advisor.Id;
            //}

            return null;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
        public async Task<bool> Any(Expression<Func<AdminLogin, bool>> where = null)
        {
            return await _context.AdminLogins.AnyAsync(where);
        }
    }
}
