using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Panel.Application.DTOs
{
    public class ChatComplectionResponseDto
    {
        public string AiResponse { get; set; }
        
        public double RequestCost { get; set; }
    }
}
