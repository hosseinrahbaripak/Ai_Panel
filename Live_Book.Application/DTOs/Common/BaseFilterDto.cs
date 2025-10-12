namespace Live_Book.Application.DTOs.Common
{
    public class BaseFilterDto
    {
        public string? SearchText { get; set; } = "";
        public int PageId { get; set; } = 1;
        public int OrderBy { get; set; }
        public int Take { get; set; }
        public DateTime? DateTime { get; set; }
        public string? DateTimeStr { get; set; }
    }
}
