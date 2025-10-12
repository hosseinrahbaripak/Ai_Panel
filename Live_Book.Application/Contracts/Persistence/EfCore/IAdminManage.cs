using Live_Book.Domain;
using System.Linq.Expressions;

namespace Live_Book.Application.Contracts.Persistence.EfCore
{
    public interface IAdminManage : IAsyncDisposable
    {
        Task<List<AdminLogin>> GetAll(Expression<Func<AdminLogin, bool>> where = null, int skip = 0, int take = int.MaxValue);
        Task<int> GetCount(Expression<Func<AdminLogin, bool>> where = null);
        Task<AdminLogin> FirstOrDefault(Expression<Func<AdminLogin, bool>> @where = null);
        Task<AdminLogin> Add(AdminLogin admin);
        Task Update(AdminLogin login);
        Task DeleteAdminLogin(AdminLogin admin);
        Task DeleteAdminLogin(int id);
        Task<int?> GetAdminProfileId(int adminId);
        Task<bool> Any(Expression<Func<AdminLogin, bool>> @where = null);
    }
}
