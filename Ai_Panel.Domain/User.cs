using Ai_Panel.Domain;
using Ai_Panel.Domain.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Domain
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [NotMapped]
        public string? FullName => !string.IsNullOrEmpty(FirstName + " " + LastName) ? FirstName + " " + LastName : "";

        [MaxLength(150)]
        [Display(Name = "نام")]
        public string? FirstName { get; set; }

        [MaxLength(150)]
        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; }

        [MaxLength(150)]
        [Display(Name = "جنسیت")]
        public Gender? Gender { get; set; }

        [Display(Name = "کد ملی")]
        public string? NationalId { get; set; }

        [MaxLength(5)]
        [Display(Name = "کد فعالسازی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? ActiveCode { get; set; }

        [MaxLength(11)]
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string MobileNumber { get; set; }

        [Display(Name = "عکس پروفایل")]
        public string? Avatar { get; set; }

        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "ایمیل")]
        public string? Email { get; set; }

        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string? Password { get; set; }

        public string? PassKey { get; set; }

        [Display(Name = "اکانت پریمیوم")]
        public bool IsPremiumAccount { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime DateTime { get; set; }

        [Display(Name = "تاریخ آپدیت")]
        public DateTime UpdateDateTime { get; set; }

        public UserTypeEnum UserType { get; set; }

        public bool IsDelete { get; set; }
        public bool HasAccessToAiChat { get; set; }
        public List<UserAiChatLog>? UserAiChatLogs { get; set; }
        public List<UserSession> UserSessions { get; set; }
        public List<UserService>? UserServices {  get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
