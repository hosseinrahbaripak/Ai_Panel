using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Domain
{
	public class UserSession
	{
		[Key]
		public int Id { get; set; }

		public int UserId { get; set; }

		[Required]
		public string Token { get; set; }

		[Display(Name = "تاریخ ورود")]
		public DateTime DateTime { get; set; }

		[Display(Name = "تاریخ خروج")]
		public DateTime? DateLogout { get; set; }

		[Display(Name = "وضعیت")]
		public bool IsLogout { get; set; }

		[ForeignKey("UserId")]
		public User Users { get; set; }
	}
}
