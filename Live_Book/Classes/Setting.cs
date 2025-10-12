using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Live_Book.Classes
{
    public class SettingMethod
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SettingMethod(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsLocal()
        {
            var url = _httpContextAccessor.HttpContext?.Request.Host.Value;
            return url == null || url.Contains("localhost");
        }
    }
}
