using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ai_Panel.Domain
{
	public class RolesInPages
	{
		[Key]
		public int RoleInPagesId { get; set; }

		[ForeignKey("Role")]
		public int RoleId { get; set; }


		[ForeignKey("Pages")]
		public int PageId { get; set; }

		[DisplayName("افزودن")]
		public bool Add { get; set; }

		[DisplayName("ویرایش")]
		public bool Edit { get; set; }

		[DisplayName("حذف")]
		public bool Delete { get; set; }

		[DisplayName("بازدید")]
		public bool Visit { get; set; }


		#region Relation
		public Role Role { get; set; }
		public Pages Pages { get; set; }
		#endregion

	}
}
