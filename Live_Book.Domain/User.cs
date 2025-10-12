using Ai_Panel.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Domain
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(350)]
        [Display(Name = "نام")]
        public string? Name { get; set; }

        [NotMapped]
        public string? FullName => !string.IsNullOrEmpty(FirstName + " " + LastName) ? FirstName + " " + LastName : Name;

        [MaxLength(150)]
        [Display(Name = "نام")]
        public string? FirstName { get; set; }

        [MaxLength(150)]
        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; }

        [MaxLength(5)]
        [Display(Name = "کد فعالسازی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ActiveCode { get; set; }

        [MaxLength(11)]
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string MobileNumber { get; set; }

        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "ایمیل")]
        public string? Email { get; set; }

        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string? Password { get; set; }

        public string? PassKey { get; set; }

        [MaxLength(10)]
        [Display(Name = "کد ملی")]
        public string? NationalCode { get; set; }

        [Display(Name = "جنسیت")]
        public int? Gender { get; set; }

        public int Status { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime DateTime { get; set; }

        [Display(Name = "تاریخ آپدیت")]
        public DateTime UpdateDateTime { get; set; }

        public bool IsDelete { get; set; }
        public bool HasAccessToAiChat { get; set; }


		[DefaultValue(null)]
		public int? AdminId { get; set; }
		[ForeignKey(nameof(AdminId))]
		public AdminLogin? Admin { get; set; }

		public List<UserSession> UserSessions { get; set; }

    }
}
