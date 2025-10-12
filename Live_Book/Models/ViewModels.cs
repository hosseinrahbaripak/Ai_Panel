using Live_Book.Domain;
using static Live_Book.TagHelpers.PagingTagHelper;

namespace Live_Book.Models
{
	public class BookIndexModel
    {
        public PagingInfo PagingInfo { get; set; }
    }

    public class UserIndexModel
    {
        public List<User> Users { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class FullInfoUserPaging
    {
        public List<RequestLogin>? RequestLogins { get; set; }
    }
}
