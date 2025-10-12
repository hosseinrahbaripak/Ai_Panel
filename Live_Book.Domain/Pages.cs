using System.ComponentModel.DataAnnotations;

namespace Live_Book.Domain
{
	public class Pages
	{
		[Key]
		public int PageId { get; set; }

		[Display(Name = "عنوان فارسی صفحه")]
		[MaxLength(100)]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string PersianPageTitle { get; set; }


		[Display(Name = "عنوان انگیلسی صفحه")]
		[MaxLength(100)]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string EnglishPageTitle { get; set; }


		[Display(Name = " آدرس صفحه")]
		[MaxLength(50)]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string PageAddress { get; set; }


		public List<RolesInPages> RolePages { get; set; }
	}
}
