using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ai_Panel.Domain.Common;

namespace Ai_Panel.Domain;

public class AiConfig : BaseCategory
{
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

	[DisplayName("پیام آغازین")]
	public string FirstMessage { get; set; }

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

	[ForeignKey(nameof(CreateBy))]
	public AdminLogin AdminCreated { get; set; }

	[ForeignKey(nameof(UpdateBy))]
	public AdminLogin AdminUpdeted { get; set; }
}
// ==> https://medium.com/nerd-for-tech/model-parameters-in-openai-api-161a5b1f8129