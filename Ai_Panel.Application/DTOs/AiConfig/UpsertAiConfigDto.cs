using Ai_Panel.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Ai_Panel.Application.DTOs.Common;
using System.ComponentModel;

namespace Ai_Panel.Application.DTOs.AiConfig;

public class UpsertAiConfigDto : BaseDto
{
    [MaxLength(250)]
	[DisplayName("عنوان")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public string Title { get; set; }

    [MaxLength(50)]
    public string Version { get; set; }
    [DisplayName("پلتفرم")]
    [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
    public int AiPlatformId { get; set; }

    [DisplayName("مدل هوش مصنوعی")]
	[Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
	public int? AiModelId { get; set; }

    [MaxLength(2000)]
	[DisplayName("پرامپت")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Prompt { get; set; }

	[Range(0, 2)]
    public float Temperature { get; set; }

    [Range(0, 1)]
    public float TopP { get; set; }

    [Range(0, 1000000)]
	[DisplayName("بیشترین تعداد کلمات خروجی ")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public int MaxTokens { get; set; }

    [MaxLength(2000)]
	//[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string? Stop { get; set; }
    public int N { get; set; } = 1;
    public float PresencePenalty { get; set; }
    public float FrequencyPenalty { get; set; }
    public int CreateBy { get; set; }
}