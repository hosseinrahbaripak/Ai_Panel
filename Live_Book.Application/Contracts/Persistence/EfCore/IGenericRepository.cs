using System.Linq.Expressions;

namespace Live_Book.Application.Contracts.Persistence.EfCore
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<List<T>> GetAll(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int skip = 0, int take = int.MaxValue);
        Task<int> GetCount(Expression<Func<T, bool>> where = null);
        Task<bool> Exist(int id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task BulkDelete(List<T> entity);

        Task<T> FirstOrDefault(Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<T> FirstOrDefaultAsNoTracking(Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<T> LastOrDefault(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties = "");
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);
    }
}
