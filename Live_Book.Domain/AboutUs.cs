using System.ComponentModel.DataAnnotations;

namespace Live_Book.Domain
{
	public class AboutUs
	{
		[Key]
		public int Id { get; set; }


		[Display(Name = "متن")]
		[MaxLength(2000)]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string Descreption { get; set; }


		[Display(Name = "عکس")]
		[MaxLength(50)]
		public string? ImageName { get; set; }
	}
}
