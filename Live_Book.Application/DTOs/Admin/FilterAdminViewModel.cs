namespace Live_Book.Application.DTOs.Admin;

public class FilterAdminViewModel
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public int RoleId { get; set; }
    public string Title { get; set; }
    public string MobileNumber { get; set; }
    public string HelliCode { get; set; }
    public int ProjectId { get; set; }
    public string? DateStr { get; set; }
    public string? ToDateStr { get; set; }
}