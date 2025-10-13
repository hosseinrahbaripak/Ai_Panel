using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Panel.Application.Services.Ai
{
    public interface IAiApiClient
    {
        Task<string?> GetChatCompletionAsync(string prompt);
    }
}
