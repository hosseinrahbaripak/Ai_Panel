using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Domain.Common;

namespace Ai_Panel.Domain
{
    public class UserService:BaseEntity
    {
        [Key]
        public int Id {  get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int UserId {  get; set; }
        public User User { get; set; }
        public int AiServiceId {  get; set; }
        public AiService AiService { get; set; }
        public bool IsDelete {  get; set; }
        public bool IsActive  { get; set; } = true;
        public List<UserAiChatLog>? UserAiChatLogs { get; set; }
    }
}
