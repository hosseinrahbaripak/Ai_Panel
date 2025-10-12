using Live_Book.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Live_Book.Classes
{
    public class WebTools
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebTools(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public string GetRealIp()
        {
            try
            {
                if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Headers["AR_REAL_IP"]))
                    return _httpContextAccessor.HttpContext.Request.Headers["AR_REAL_IP"];
                if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Headers["REMOTE_ADDR"]))
                    return _httpContextAccessor.HttpContext.Request.Headers["REMOTE_ADDR"];
                return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public string GetBrowser()
        {
            try
            {
                var browser = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
                return browser;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public bool IsLogin()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            return claimsIdentity.IsAuthenticated;
        }
        public void SetCookieData(string key, string value)
        {
            var exist = GetCookieData(key);
            if (!string.IsNullOrEmpty(exist))
            {
                DeleteCookieData(key);
            }
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.UtcNow.AddDays(30);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, options);
        }
        public string GetCookieData(string key)
        {
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies[key] ?? "";
            return cookie;
        }
        public void DeleteCookieData(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }
        public string GetPath()
        {
            return _httpContextAccessor.HttpContext.Request.Path;
        }
        public string GetToken()
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers["Token"];
            return string.IsNullOrEmpty(header) ? "" : header.ToString();
            //var token = _httpContextAccessor.HttpContext.Request.Headers.Authorization.ToString();
            //         return token;
        }
        public List<string> GetPages()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            return claimsIdentity.FindAll(u => u.Type == CustomClaimTypes.Pages).Select(u => u.Value).ToList();
        }
        public List<string> GetRoles()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            return claimsIdentity.FindAll(u => u.Type == ClaimTypes.Role).Select(u => u.Value).ToList();
        }
        public DateTime LocalDateTime(DateTime date)
        {
            var timeZone = _httpContextAccessor.HttpContext.Request.Cookies["client_timezone"] ?? "0";
            var minToAdd = -(int.Parse(timeZone));
            date = date.AddMinutes(minToAdd);
            return date;
        }
        public string FormatDate(DateTime date)
        {
            return LocalDateTime(date).ToString("dd.MM.yyyy HH:mm");
        }
        public string FormatDateOnly(DateTime date)
        {
            return LocalDateTime(date).ToString("dd.MM.yyyy");
        }
        public string FormatDateOnlyV2(DateTime date)
        {
            return LocalDateTime(date).ToString("dd.MM.yyyy");
        }
    }
}
