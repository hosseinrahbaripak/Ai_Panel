using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_Panel.Application.DTOs.User
{
    public class VerifyOtpDto
    {
        [MaxLength(11, ErrorMessage = "شماره همراه نمی‌تواند بیش از 11 کاراکتر باشد")]
        [MinLength(11, ErrorMessage = "شماره همراه باید 11 رقمی باشد")]
        [RegularExpression(@"^09[0-9]*$", ErrorMessage = "شماره همراه باید با 09 شروع شود")]
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا شماره همراه را وارد کنید")]
        public string MobileNumber { get; set; }
        public string? Code {  get; set; }
    }
}
