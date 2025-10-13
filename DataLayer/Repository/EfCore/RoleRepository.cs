using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ai_Panel.Persistence.Repository.EfCore
{
    public class RoleRepository
    {
        private readonly LiveBookContext _context;
        public RoleRepository(LiveBookContext context)
        {
            _context = context;
        }
        //public async Task<List<Role>> GetAll(Expression<Func<Role, bool>> @where = null, int skip = 0, int take = int.MaxValue)
        //{
        //    var db = _context.Roles.AsQueryable();
        //    if (where != null)
        //    {
        //        db = db.Where(where);
        //    }
        //    return await db.Include(x => x.AdminType).Skip(skip).Take(take).ToListAsync();
        //}
        //public async Task<Role> FirstOrDefault(Expression<Func<Role, bool>> where = null)
        //{
        //    if (where != null)
        //    {
        //        return await _context.Roles.FirstOrDefaultAsync(where);
        //    }
        //    return null;
        //}
        //public async Task<AdminLogin> FirstOrDefaultAdmin(Expression<Func<AdminLogin, bool>> where = null)
        //{
        //    if (where != null)
        //    {
        //        return await _context.AdminLogins.Include(x => x.Role)
        //            .Where(x => x.IsDelete == false).FirstOrDefaultAsync(where);
        //    }
        //    return null;
        //}
        //public async Task<int> GetCount()
        //{
        //    return await _context.Roles.CountAsync();
        //}
        //public async Task AddRole(Role role)
        //{
        //    await _context.AddAsync(role);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task RemoveRole(int id)
        //{
        //    var role = await FindRoleById(id);
        //    _context.Roles.Remove(role);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task<Role> FindRoleById(int id)
        //{
        //    return await _context.Roles.Include(u => u.AdminRoles).Where(r => r.RoleId == id).FirstOrDefaultAsync();
        //}
        //public async Task UpdateRole(Role role)
        //{
        //    _context.Roles.Update(role);
        //    await _context.SaveChangesAsync();
        //}
        //public async ValueTask DisposeAsync()
        //{
        //    await _context.DisposeAsync();
        //}
        //public async Task<int> FindRoleByUserName(string userName)
        //{
        //    var res = await _context.AdminLogins.FirstOrDefaultAsync(u => u.UserName == userName);
        //    return res.RoleId;
        //}
        //public async Task<bool> CheckPermissionAction(string userName, string pageUrl, string action)
        //{
        //    int RoleId = await FindRoleByUserName(userName);
        //    var IsExistAdminWithPermission = await _context.AdminLogins.Where(u => u.UserName == userName && u.RoleId == RoleId).ToListAsync();
        //    if (IsExistAdminWithPermission.Count == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        var RolePermission = await _context.RolesInPages.Include(u => u.Pages)
        //            .FirstOrDefaultAsync(u => u.RoleId == RoleId && u.Pages.EnglishPageTitle == pageUrl);
        //        if (RolePermission == null)
        //        {
        //            return false;
        //        }
        //        if (action == "Add" && RolePermission.Add)
        //        {
        //            return true;
        //        }
        //        if (action == "Edit" && RolePermission.Edit)
        //        {
        //            return true;
        //        }
        //        if (action == "Index" && RolePermission.Visit)
        //        {
        //            return true;
        //        }
        //        if (action == "Delete" && RolePermission.Delete)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}
        //public async Task<List<Pages>> GetAllPages(Expression<Func<Pages, bool>> @where = null)
        //{
        //    if (@where != null)
        //    {
        //        return await _context.Pages.Include(x => x.RolePages).Where(where).ToListAsync();
        //    }
        //    return await _context.Pages.Include(x => x.RolePages).ToListAsync();
        //}
        //public async Task AddRoleInRolePages(RolesInPages roles)
        //{
        //    await _context.RolesInPages.AddAsync(roles);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task<List<RolesInPages>> GetAllRoleInPages(Expression<Func<RolesInPages, bool>> @where = null)
        //{
        //    return await _context.RolesInPages.Include(u => u.Pages).Where(where).ToListAsync();
        //}
        //public async Task<RolesInPages> FindRoleInPage(int RoleInpageId)
        //{
        //    return await _context.RolesInPages.Include(u => u.Pages).SingleAsync(u => u.RoleInPagesId == RoleInpageId);
        //}
        //public async Task DeleteRoleInPage(int id)
        //{
        //    // id == roleinpageId
        //    var roleInpage = await FindRoleInPage(id);
        //    if (roleInpage != null)
        //    {
        //        _context.RolesInPages.Remove(roleInpage);
        //        await _context.SaveChangesAsync();
        //    }

        //}
        //public async Task UpdateRoleInPage(RolesInPages rolesInPages)
        //{
        //    _context.Update(rolesInPages);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task SaveChange()
        //{
        //    await _context.SaveChangesAsync();
        //}
        //public async Task<string[]> GetPagesByUserName(string username)
        //{
        //    var role = await FindRoleByUserName(username);
        //    return await _context.RolesInPages.Include(u => u.Pages).Where(u => u.RoleId == role)
        //         .Select(u => u.Pages.EnglishPageTitle).ToArrayAsync();
        //}
        //public async Task<string> GetRoleNameById(int roleid)
        //{
        //    return await _context.Roles.Where(r => r.RoleId == roleid).Select(r => r.RoleTitle).SingleAsync();
        //}
        //public async Task<RolesInPages> FirstOrDefaultRoleInPages(Expression<Func<RolesInPages, bool>> where = null)
        //{
        //    if (where != null)
        //    {
        //        return await _context.RolesInPages.FirstOrDefaultAsync(where);
        //    }
        //    return null;
        //}
    }
}
