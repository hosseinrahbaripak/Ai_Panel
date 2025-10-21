using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Panel.Application.DTOs.AiConfig
{
    public class AiConfigGroupDto
    {
        public int GroupId { get; set; }
        public string Title { get; set; }
        public List<string> Modles { get; set; } = new();
    }
}
