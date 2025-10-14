using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Domain;

namespace Ai_Panel.Application.DTOs.AiChat
{
    public class ChatCompletionDto
    {
        public string BaseUrl { get; set; }
        public string Model { get; set; }
        public string Prompt { get; set; }
        public string Message { get; set; }
        public float? Temperature { get; set; } = 1.0f;
        public float? TopP { get; set; } = 1.0f;
        public int? MaxTokens { get; set; } = 10000;
        public string[]? Stop { get; set; } = null;
        public int? N { get; set; } = 1;
        public float? PresencePenalty { get; set; } = 0.0f;
        public float? FrequencyPenalty { get; set; } = 0.0f;

    }
}
