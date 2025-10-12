using System.ComponentModel.DataAnnotations;

namespace Live_Book.Domain
{
	public class ContactUs
	{
		public int Id { get; set; }

		[Display(Name = "آدرس")]
		[MaxLength(800)]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string Address { get; set; }

		[Display(Name = "شماره تماس")]
		[MaxLength(200)]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string PhoneNumber { get; set; }


		[Display(Name = "فکس")]
		[MaxLength(200)]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string Fax { get; set; }


		[Display(Name = "لوگو")]
		[MaxLength(50)]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string LogoName { get; set; }


	}
}
