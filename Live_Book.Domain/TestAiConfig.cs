using Ai_Panel.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Domain;

public class TestAiConfig : BaseEntity
{
    public int AdminId { get; set; }
    public AdminLogin Admin { get; set; }
    public int AiId { get; set; }
    [ForeignKey(nameof(AiId))]
    public AiModel Ai { get; set; }
    public int AiModelId { get; set; }
    [ForeignKey(nameof(AiModelId))]
    public AiModel AiModel { get; set; }
    public int MaxTokens { get; set; }
    [MaxLength(1500)]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Message { get; set; }
    public string? BookContent { get; set; }

    [MaxLength(2000)]
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
}