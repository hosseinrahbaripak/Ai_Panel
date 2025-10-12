using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ai_Panel.Application.DTOs.AiConfig;
public class AddAiConfigToQuestionDto
{
	[Required]
	public int AiConfigId { get; set; }
	[DisplayName("کتاب‌(ها)")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public List<int> BookIds { get; set; }
	[DisplayName("فصل‌ها")]
	public List<int>? PartIds { get; set; }
	[DisplayName("سوالات")]
	public List<int>? QuestionIds { get; set; }
}
