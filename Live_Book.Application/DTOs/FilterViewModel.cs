using Live_Book.Application.Enum;

namespace Live_Book.Application.DTOs
{
	public class UsersFilter
	{
		public string? MobileNumber { get; set; }
		public string? Name { get; set; }
		public string? HelliCode { get; set; }
		public int? GradeId { get; set; }
		public List<int> ParentUserTagsId { get; set; }
		public List<int> UserTagsId { get; set; }
		public string? DateStr { get; set; }
		public string? ToDateStr { get; set; }
		public int? ProjectProfileId { get; set; }
		public int? AdvisorId { get; set; }
		public int? ParentAdvisorId { get; set; }
        public UserTypeEnum? UserType { get; set; }
        public int? BookId { get; set; }
    }
	public class ReportFirstUserBookReadFilter
	{
		public string? MobileNumber { get; set; }
		public string? Name { get; set; }
		public string? HelliCode { get; set; }
		public int? GradeId { get; set; }
        public List<int> ParentUserTagsId { get; set; }
        public List<int> UserTagsId { get; set; }
        public int? ProjectId { get; set; }
        public int? ParentAdvisorId { get; set; }
        public int? AdvisorId { get; set; }
        public string? DateStr { get; set; }
		public string? ToDateStr { get; set; }
	}
	public class RequestLoginFilter
	{
        public string? Name { get; set; }
        public string? MobileNumber { get; set; }
		public string? HelliCode { get; set; }
		public string? DateStr { get; set; }
		public string? ToDateStr { get; set; }
		public int Order { get; set; } = 0;
		public int? ProjectId { get; set; }
		public int? ParentAdvisorId { get; set; }
		public int? AdvisorId { get; set; }
		public List<int> ParentUserTagsId { get; set; }
		public List<int> UserTagsId { get; set; }
		public UserTypeEnum? UserType { get; set; }
		public int? Status { get; set; }
    }

	public class AdminActionFilterDp
	{
		public string? AdminName { get; set; }
		public string? UserName { get; set; }
		public string? AdminUserName { get; set; }
		public string? DateStr { get; set; }
		public string? ToDateStr { get; set; }
	}

	public class TicketFilterModel
	{
		public int? Status { get; set; }
		public string? Name { get; set; }
		public string? HelliCode { get; set; }
		public int? GradeId { get; set; }
		public string? DateStr { get; set; }
		public string? ToDateStr { get; set; }
	}
}
