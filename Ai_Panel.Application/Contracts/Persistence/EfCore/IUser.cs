using System.Linq.Expressions;
using Ai_Panel.Domain;

namespace Ai_Panel.Application.Contracts.Persistence.EfCore
{
    public interface IUser : IAsyncDisposable
    {
        Task<List<User>> GetAll(Expression<Func<User, bool>> where = null, int skip = 0, int take = int.MaxValue);
  //      Task<List<GetUsersForAdminDto>> GetAllForAdmin(Expression<Func<User, bool>> where = null, int skip = 0, int take = int.MaxValue);
  //      Task<List<User>> GetAllDeleted(Expression<Func<User, bool>> where = null, int skip = 0, int take = int.MaxValue);
  //      Task<int> GetUserReadLogsCount(DateTime? start, DateTime? end, Expression<Func<User, bool>> where = null);
  //      Task<int> GetCount(Expression<Func<User, bool>> where = null);
  //      Task<int> GetCountDeleted(Expression<Func<User, bool>> where = null);
        Task<User> FirstOrDefault(Expression<Func<User, bool>> where, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "");
        Task<bool> Any(Expression<Func<User, bool>> where = null);
  //      Task<int> Add(User user);
        Task Upsert(User users);
  //      Task Delete(int userId);
  //      Task Delete(User user);
  //      Task<string> CreateToken(int userId);
  //      Task<string> EditToken(int userId);
  //      Task<UserSession> GetByToken(string token);
  //      Task<int> GetTotalInstallationCount(Expression<Func<UserSession, bool>> where = null);
  //      Task<string> Login(string mobile);
  //      Task<string> LoginV3(string mobile, string helliCode);
  //      Task<Tuple<string?, int?>> LoginV4(string mobile, string? helliCode, bool createUser = false);
  //      Task<UserViewModel> Activation(string mobile, string activeCode);
  //      Task<ServiceMessage> ActivationV4(string mobile, string activeCode, string? helliCode);
  //      Task<UserViewModel> UpdateProfile(string token, UserViewModel model);
  //      Task DeActiveSession(int sessionId);
		//Task DisConnectAdvisor(int advisorId);
		////Task MyQuery(List<string> HelliCodes);
		//Task MyQuery();



    }
}
