using System.ComponentModel.DataAnnotations;

namespace Live_Book.Domain.Common
{
    /// <summary>
    /// This the base column in all table 
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int? CreateBy { get; set; }
        public int? UpdateBy { get; set; }
        public bool IsDelete { get; set; }
    }

}
