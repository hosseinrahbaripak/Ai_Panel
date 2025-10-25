using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ai_Panel.Domain.Common;

namespace Ai_Panel.Domain;

public class AiConfig : BaseCategory
{
	[Key]
    public int Id { get; set; }

    [MaxLength(50)]
	[DisplayName("نسخه")]
	public string Version { get; set; }
    public int? AiPlatformId { get; set; }
    [ForeignKey(nameof(AiPlatformId))]
    public AiPlatform? AiPlatform { get; set; }
    public int AiModelId { get; set; }
	[ForeignKey(nameof(AiModelId))]
	public AiModel AiModel { get; set; }

	[MaxLength(2000)]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	[DisplayName("پرامپت")]
	public string Prompt { get; set; }

	[Range(0, 2)]
	public float Temperature { get; set; }

	[Range(0, 1)]
	public float TopP { get; set; }

	[Range(0, 1000000)]
	public int MaxTokens { get; set; }
	public string[] Stop { get; set; }
	public int N { get; set; }
	[Range(-2, 2)]
	public float PresencePenalty { get; set; }
	[Range(-2, 2)]
	public float FrequencyPenalty { get; set; }
	public int? AiConfigOrder { get; set; }
	public int? AiConfigGroupId { get; set; }
    public AiConfigGroup? AiConfigGroup { get; set; }
	public int? ContractTemplateId { get; set; }
    public ContractTemplate? ContractTemplate { get; set; }

}