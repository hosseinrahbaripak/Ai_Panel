using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ai_Panel.Persistence.Repository.EfCore
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LiveBookContext _context;

        public GenericRepository(LiveBookContext context)
        {
            _context = context;
        }
        public virtual async Task<T> Add(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task BulkDelete(List<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<T> FirstOrDefault(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
                query = orderBy(query);
            return await query.FirstOrDefaultAsync(where);
        }

        public virtual async Task<T> FirstOrDefaultAsNoTracking(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
                query = orderBy(query);
            return await query.AsNoTracking().FirstOrDefaultAsync(where);
        }

        public virtual async Task<T> LastOrDefault(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await orderBy(query).LastOrDefaultAsync(where);
        }
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return await _context.Set<T>().AnyAsync(where);
        }
        public virtual async Task<List<T>> GetAll(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int skip = 0, int take = int.MaxValue)
        {
            var dbSet = _context.Set<T>();
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.Skip(skip).Take(take).ToListAsync();
        }
        public virtual async Task<int> GetCount(Expression<Func<T, bool>> where = null)
        {
            if (where == null)
            {
                return await _context.Set<T>().CountAsync();
            }
            return await _context.Set<T>().CountAsync(where);
        }

        public virtual async Task<bool> Exist(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public virtual async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}