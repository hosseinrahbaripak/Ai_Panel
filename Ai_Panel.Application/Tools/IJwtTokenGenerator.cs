using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Panel.Application.Tools
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(int userId);
    }
}
