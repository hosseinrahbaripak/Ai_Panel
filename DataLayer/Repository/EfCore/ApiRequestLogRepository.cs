using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ai_Panel.Persistence.Repository.EfCore
{
    public class ApiRequestLogRepository : IApiRequestLog
    {
        #region Ctor
        private readonly AiPanelContext _context;
        private readonly IErrorLog _log;
        public ApiRequestLogRepository(AiPanelContext context, IErrorLog log)
        {
            _context = context;
            _log = log;
        }
        #endregion

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
        public async Task<List<ApiRequestLog>> GetAll(Expression<Func<ApiRequestLog, bool>> where = null, int skip = 0, int take = int.MaxValue)
        {
            if (where == null)
            {
                return await _context.ApiRequestLogs.Skip(skip).Take(take).ToListAsync();
            }
            return await _context.ApiRequestLogs.Where(where).Skip(skip).Take(take).ToListAsync();
        }
        public async Task Add(ApiRequestLog apiRequestLog)
        {
            try
            {
                await _context.AddAsync(apiRequestLog);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await _log.Add(e.Message, e.InnerException?.Message ?? "", "Add In ApiRequestLog");
                throw;
            }
        }

        public async Task<bool> Delete(ApiRequestLog apiRequestLog)
        {
            try
            {
                _context.ApiRequestLogs.Remove(apiRequestLog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                await _log.Add(e.Message, e.InnerException?.Message ?? "", "Delete In ApiRequestLog");
                return false;
            }
        }

        public async Task<bool> BulkDelete(List<ApiRequestLog> apiRequestLog)
        {
            try
            {
                _context.ApiRequestLogs.RemoveRange(apiRequestLog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                await _log.Add(e.Message, e.InnerException?.Message ?? "", "Delete In ApiRequestLog");
                return false;
            }
        }
    }
}