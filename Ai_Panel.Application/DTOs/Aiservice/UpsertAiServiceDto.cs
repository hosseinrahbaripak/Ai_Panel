using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Panel.Application.DTOs.Aiservice
{
    public class UpsertAiServiceDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsRecommended { get; set; } = false;
        public int AiConfigId { get; set; }
        public Domain.AiConfig AiConfig { get; set; }
    }
}
