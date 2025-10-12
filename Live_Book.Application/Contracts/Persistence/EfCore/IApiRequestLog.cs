using Live_Book.Domain;
using System.Linq.Expressions;

namespace Live_Book.Application.Contracts.Persistence.EfCore
{
    public interface IApiRequestLog : IAsyncDisposable
    {
        Task<List<ApiRequestLog>> GetAll(Expression<Func<ApiRequestLog, bool>> @where = null, int skip = 0, int take = int.MaxValue);
        Task Add(ApiRequestLog apiRequestLog);
        Task<bool> Delete(ApiRequestLog apiRequestLog);
        Task<bool> BulkDelete(List<ApiRequestLog> apiRequestLog);
    }
}