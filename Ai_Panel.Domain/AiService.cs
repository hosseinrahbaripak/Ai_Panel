using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Domain.Common;
namespace Ai_Panel.Domain
{

    public class AiService : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsRecommended { get; set; } = false;
        public int AiConfigGroupId { get; set; }
        public AiConfigGroup AiConfigGroup { get; set; }
        public List<UserService>? UserService { get; set; }
    }
}