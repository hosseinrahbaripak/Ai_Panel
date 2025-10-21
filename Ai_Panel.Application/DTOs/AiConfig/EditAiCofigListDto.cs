using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Panel.Application.DTOs.AiConfig
{
    public class EditAiCofigListDto:AddAiConfigListDto
    {
        public int? Id { get; set; }
        public int? GroupId { get; set; }
    }
}
