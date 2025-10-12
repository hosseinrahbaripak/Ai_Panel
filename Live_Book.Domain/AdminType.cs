using Live_Book.Domain.Common;

namespace Live_Book.Domain;
public class AdminType : BaseCategory
{
	#region Relation
	public List<Role>? Roles { get; set; }
	#endregion
}