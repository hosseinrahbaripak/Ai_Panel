using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Domain;

namespace Ai_Panel.Application.DTOs.Aiservice
{
    public class UpsertAiServiceDto
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsRecommended { get; set; } = false;
        public int AiConfigGroupId { get; set; }
        public AiConfigGroup AiConfigGroup { get; set; }
    }
}
