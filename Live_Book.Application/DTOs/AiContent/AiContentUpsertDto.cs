using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Live_Book.Application.DTOs.Common;
using Live_Book.Domain;

namespace Live_Book.Application.DTOs.AiContent;

public class AiContentUpsertDto : BaseDto
{
	[DisplayName("محتوا")]
	[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
	public string Content { get; set; }

    public int AiConfigId { get; set; }
}