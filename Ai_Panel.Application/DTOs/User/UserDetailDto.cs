using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Domain.Enum;

namespace Ai_Panel.Application.DTOs.User
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public string? NationalId { get; set; }
        public string MobileNumber { get; set; }
        public string? Avatar { get; set; }
        public bool IsPremiumAccount { get; set; }
        public DateTime DateTime { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string Token { get; set; }
    }
}
