using System.ComponentModel.DataAnnotations;

namespace Ai_Panel.Domain.Common
{
    /// <summary>
    ///  For model that has Title column
    /// </summary>
    public abstract class BaseCategory : BaseEntity
    {
        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string? Image { get; set; }
    }
}
