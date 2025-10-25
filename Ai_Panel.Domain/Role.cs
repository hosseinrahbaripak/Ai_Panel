using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Domain
{
    public class Role
	{
		[Key]
		public int RoleId { get; set; }

		[Display(Name = "نقش")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0}نمیتواند بشتر از {1} کاراکتر باشد .")]
		public string RoleTitle { get; set; }
        public List<UserRole>? UserRoles { get; set; }
    }
}
