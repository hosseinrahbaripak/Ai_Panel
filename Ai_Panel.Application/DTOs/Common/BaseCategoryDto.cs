using System.ComponentModel.DataAnnotations;

namespace Ai_Panel.Application.DTOs.Common
{
    /// <summary>
    ///  For model that has Title column
    /// </summary>
    public abstract class BaseCategoryDto : BaseDto
    {
        [MaxLength(250)]
        public string Title { get; set; }
        public string? Image { get; set; }
    }
}
