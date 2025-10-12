using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Ai_Panel.Application.DTOs.Common;
using Ai_Panel.Domain;

namespace Ai_Panel.Application.DTOs.AiContent;

public class AiContentUpsertDto : BaseDto
{
	[DisplayName("محتوا")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public string Content { get; set; }

    public int AiConfigId { get; set; }
}