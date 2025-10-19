using System.ComponentModel.DataAnnotations;

namespace Ai_Panel.Application.DTOs.User
{
    public class RegisterUserDto
    {
        [MaxLength(150, ErrorMessage = "نام نمی‌تواند بیش از 150 کاراکتر باشد")]
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        public string FirstName { get; set; }

        [MaxLength(150, ErrorMessage = "نام خانوادگی نمی‌تواند بیش از 150 کاراکتر باشد")]
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        public string LastName { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی باید 10 رقمی باشد")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "کد ملی باید فقط شامل اعداد باشد")]
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا کد ملی را وارد کنید")]
        public string NationalId { get; set; }

        [MaxLength(11, ErrorMessage = "شماره همراه نمی‌تواند بیش از 11 کاراکتر باشد")]
        [MinLength(11, ErrorMessage = "شماره همراه باید 11 رقمی باشد")]
        [RegularExpression(@"^09[0-9]*$", ErrorMessage = "شماره همراه باید با 09 شروع شود")]
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا شماره همراه را وارد کنید")]
        public string MobileNumber { get; set; }

    }
}
