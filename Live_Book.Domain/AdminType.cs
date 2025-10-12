using Ai_Panel.Domain.Common;

namespace Ai_Panel.Domain;
public class AdminType : BaseCategory
{
	#region Relation
	public List<Role>? Roles { get; set; }
	#endregion
}