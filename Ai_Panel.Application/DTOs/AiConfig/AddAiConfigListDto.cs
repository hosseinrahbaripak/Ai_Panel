using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Panel.Application.DTOs.AiConfig
{
    public class AddAiConfigListDto
    {
        public string Title { get; set; }
        public int AiPlatformId { get; set; }
        public int AiTypeId { get; set; }
        public int AiModelId { get; set; }
        public float Temperature { get; set; }
        public float PresencePenalty { get; set; }
        public float TopP { get; set; }
        public float FrequencyPenalty { get; set; }
        public int MaxTokens { get; set; }
        public string[] Stop { get; set; }
        public string Prompt { get; set; }
        public int AiConfigOrder { get; set; }
    }
}
