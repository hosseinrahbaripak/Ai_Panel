using System.ComponentModel.DataAnnotations;

namespace Live_Book.Application.DTOs.Common
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
