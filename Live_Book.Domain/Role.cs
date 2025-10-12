using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Live_Book.Domain
{
    public class Role
	{
		[Key]
		public int RoleId { get; set; }


		[Display(Name = "نقش")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(200, ErrorMessage = "{0}نمیتواند بشتر از {1} کاراکتر باشد .")]
		public string RoleTitle { get; set; }


		#region Relation
		public List<AdminLogin>? AdminRoles { get; set; }
		public List<RolesInPages>? RolePages { get; set; }

		public int? AdminTypeId { get; set; }
		[ForeignKey(nameof(AdminTypeId))]
		public AdminType? AdminType { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Role? Parent { get; set; }
        public List<Role>? Roles { get; set; }

        #endregion
    }
}
