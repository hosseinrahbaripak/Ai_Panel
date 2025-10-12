using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Domain
{
    public class RequestLogin
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        [Required]
        [Display(Name = "تاریخ درخواست")]
        public DateTime DateReq { get; set; }

        [Display(Name = "وضعیت درخواست")]
        public int Status { get; set; }

        [Required]
        [Display(Name = "شماره تلفن")]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Display(Name = "حلی کد")]
        [MaxLength(11)]
        public string? HelliCode { get; set; }

        [MaxLength(10)]
        public string? ActiveCode { get; set; }

        public User? User { get; set; }
    }
}
