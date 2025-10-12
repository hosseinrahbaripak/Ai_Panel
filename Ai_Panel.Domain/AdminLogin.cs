using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Domain
{
    public class AdminLogin
    {
        [Key]
        public int LoginID { get; set; }

        [Display(Name = "نقش")]
        [ForeignKey("Role")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int RoleId { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250)]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        [StringLength(200, ErrorMessage = "حداقل 8 کاراکتر وارد کنید", MinimumLength = 8)]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%&/=?_.]).*$", ErrorMessage = "لطفا از حروف بزرگ ، کاراکتر خاص مثل @!$# و حداقل 8 کاراکتر استفاده کنید")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10)]
        public string Key { get; set; }

        public bool IsDelete { get; set; }

        public bool IsSuperAdmin { get; set; } = false;

        #region Relation
        public Role Role { get; set; }

		[DefaultValue(null)]
		public int? UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public User? User { get; set; }
		#endregion
	}
}
