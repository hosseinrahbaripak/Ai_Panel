using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Live_Book.Application.DTOs
{
    public class AdminPostUserModel
    {
        public int UserId { get; set; }

        [DisplayName("نام")]
        public string? FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }

        [DisplayName("شماره همراه")]
        public string MobileNumber { get; set; }

        [DisplayName("حلی کد")]
        public string? HelliCode { get; set; }

        [DisplayName("پایه")]
        public int? Grade { get; set; }
        [DisplayName("پروژه")]
        public int? ProjectId { get; set; }

        [DisplayName("جنسیت")]
        public int? Gender { get; set; }

        [DisplayName("کتاب های مجاز")]
        public List<int>? BooksId { get; set; }

		[DisplayName("تگ های کاربر")]
		public List<int>? UserTagsId { get; set; }
    }
    public class AdminPostAdminModel
    {
        public int AdminId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
		public string Password { get; set; }
    }
    public class AdvisorPostAdminModel : AdminPostAdminModel
    {
        public int AdvisorId { get; set; }
        public string Title { get; set; }
        public int ProjectId { get; set; }
        public int? AdminUserId  { get; set; }
    }
    public class ChangePass
    {
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string OldPass { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(200, ErrorMessage = "حداقل 8 کاراکتر وارد کنید", MinimumLength = 8)]
        [RegularExpression(@"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%&/=?_.]).*$", ErrorMessage = "لطفا از حروف بزرگ ، کاراکتر خاص مثل @!$# و حداقل 8 کاراکتر استفاده کنید")]
		public string Pass { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Compare("Pass", ErrorMessage = "کلمه عبور و تکرار آن یکی نیستند.")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RePass { get; set; }
    }
    public class AdminActionViewModel
    {
        public int Id { get; set; }

        public int AdminId { get; set; }
        [DisplayName("نام ادمین")]
        public string AdminUserName { get; set; }

        public int TypeId { get; set; }

        //[DisplayName("")]
        public int ObjectId { get; set; }
        public string? UserName { get; set; }
        public string? AdminName { get; set; }

        [DisplayName("صفحه")]
        public string Page { get; set; }

        [DisplayName("عملیات")]
        public string Action { get; set; }

        [DisplayName("تاریخ")]
        public DateTime DateTime { get; set; }

        public int Total_Count { get; set; }

        public string Data { get; set; }
        public string PreviousData { get; set; }
    }
    public class FindAdminActionData
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string? Data { get; set; }
        public string? PreviousData { get; set; }
        public string Action { get; set; }
    }
}
