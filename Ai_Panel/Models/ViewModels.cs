using Ai_Panel.Domain;
using static Ai_Panel.TagHelpers.PagingTagHelper;

namespace Ai_Panel.Models
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
