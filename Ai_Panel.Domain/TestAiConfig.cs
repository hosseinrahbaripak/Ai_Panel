using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ai_Panel.Domain.Common;

namespace Ai_Panel.Domain
{
    public class TestAiConfig:BaseEntity
    {
        public int userId { get; set; }
        public User User { get; set; }
        public int AiModelId { get; set; }
        [ForeignKey(nameof(AiModelId))]
        public AiModel AiModel { get; set; }
        public int MaxTokens { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Message { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Prompt { get; set; }
        public int N { get; set; }
        public float Temperature { get; set; }
        public float TopP { get; set; }
        public float PresencePenalty { get; set; }
        public float FrequencyPenalty { get; set; }
        public string[] Stop { get; set; }
        public double SummarizationCost { get; set; }
        public double RequestCost { get; set; }
        public double EmbeddingCost { get; set; }
        public string? AiResponse { get; set; }
        public bool FinalResponse {  get; set; }
        public int? ContractTemplateId { get; set; }
        public ContractTemplate? ContractTemplate { get; set; }
    }
}
