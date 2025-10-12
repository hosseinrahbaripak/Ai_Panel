using Ai_Panel.Domain;
using System.Linq.Expressions;

namespace Ai_Panel.Application.Contracts.Persistence.EfCore
{
	public interface IRequestLogin : IAsyncDisposable
	{
		Task<List<RequestLogin>> GetAll(Expression<Func<RequestLogin, bool>> where = null, int skip = 0, int take = int.MaxValue, int order = 1);
		Task Add(RequestLogin requestLogin);
		Task Update(RequestLogin requestLogin);
		Task<RequestLogin> LastOrDefault(Expression<Func<RequestLogin, bool>> where = null);
		Task<int> Count(Expression<Func<RequestLogin, bool>> where = null, int order = 1);
	}
}
