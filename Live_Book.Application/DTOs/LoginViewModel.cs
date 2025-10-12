using System.ComponentModel.DataAnnotations;

namespace Live_Book.Application.DTOs
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا به مدت 3 روز به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11)]
        public string MobileNumber { get; set; }
    }
    public class Activation
    {
        public string MobileNumber { get; set; }
        public string ActiveCode { get; set; }
    }
    public class UserViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? NationalCode { get; set; }
        public int? Gender { get; set; }
        public int? Grade { get; set; }
        public int? CityId { get; set; }
        public int? TownId { get; set; }
        public string? Token { get; set; }
        public DateTime? DateTime { get; set; }
        public bool HasAccessToAiChat { get; set; }
	}
	public class UserLoginReqViewModel
    {
        public string MobileNumber { get; set; }
        public string? ReagentCode { get; set; }
    }
    public class LoginForShopViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string MobileNumber { get; set; }
    }
}