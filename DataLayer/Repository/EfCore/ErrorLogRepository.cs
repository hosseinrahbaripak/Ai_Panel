using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Domain;
using Ai_Panel.Persistence.Configurations;

namespace Ai_Panel.Persistence.Repository.EfCore
{
	public class ErrorLogRepository : IErrorLog
    {
        private readonly LiveBookContext _context;

        public ErrorLogRepository(LiveBookContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<string> Add(string message, string? innerMessage, string action)
        {
            var token = Guid.NewGuid();
            await _context.ErrorLogs.AddAsync(new ErrorLog()
            {
                DateTime = DateTime.UtcNow.AddHours(3.5),
                Message = message,
                Action = action,
                Token = token,
                InnerMessage = innerMessage
            });
            await _context.SaveChangesAsync();
            return token.ToString();
        }
    }
}
