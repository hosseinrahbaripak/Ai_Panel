using Live_Book.Domain.Enum;

namespace Live_Book.Classes;

public class AdminLoggedIn
{
    public int AdminLoginId { get; set; }
    public int? UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public AdminTypeIdEnum AdminType { get; set; }
    public int AdminProfileId { get; set; }
    public string Pages { get; set; }
    public List<int> ProjectIds { get; set; }
	public bool HasAccessToAllProjects { get; set; }
	public bool IsSuperAdmin { get; set; }
}