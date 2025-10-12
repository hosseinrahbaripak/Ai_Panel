using Ai_Panel.Domain;
using System.Linq.Expressions;

namespace Ai_Panel.Application.Contracts.Persistence.EfCore
{
    public interface IApiRequestLog : IAsyncDisposable
    {
        Task<List<ApiRequestLog>> GetAll(Expression<Func<ApiRequestLog, bool>> @where = null, int skip = 0, int take = int.MaxValue);
        Task Add(ApiRequestLog apiRequestLog);
        Task<bool> Delete(ApiRequestLog apiRequestLog);
        Task<bool> BulkDelete(List<ApiRequestLog> apiRequestLog);
    }
}