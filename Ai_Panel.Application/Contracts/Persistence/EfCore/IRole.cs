using Ai_Panel.Domain;
using System.Linq.Expressions;

namespace Ai_Panel.Application.Contracts.Persistence.EfCore
{
	public interface IRole : IAsyncDisposable
	{
		Task<List<Role>> GetAll(Expression<Func<Role, bool>> @where = null, int skip = 0, int take = int.MaxValue);
		Task<Role> FirstOrDefault(Expression<Func<Role, bool>> @where = null);
		Task<AdminLogin> FirstOrDefaultAdmin(Expression<Func<AdminLogin, bool>> @where = null);
		Task<int> GetCount();
		Task AddRole(Role role);
		Task RemoveRole(int id);
		Task UpdateRole(Role role);
		Task<Role> FindRoleById(int id);
		Task<int> FindRoleByUserName(string userName);
		Task<bool> CheckPermissionAction(string userName, string pageUrl, string action);
		Task<List<Pages>> GetAllPages(Expression<Func<Pages, bool>> @where = null);
		Task AddRoleInRolePages(RolesInPages roles);
		Task<List<RolesInPages>> GetAllRoleInPages(Expression<Func<RolesInPages, bool>> @where = null);
		Task<RolesInPages> FindRoleInPage(int RoleInpageId);
		Task DeleteRoleInPage(int RoleInpageId);
		Task UpdateRoleInPage(RolesInPages rolesInPages);
		Task SaveChange();
		Task<string[]> GetPagesByUserName(string username);
		Task<string> GetRoleNameById(int roleid);
		Task<RolesInPages> FirstOrDefaultRoleInPages(Expression<Func<RolesInPages, bool>> where = null);
	}
}
