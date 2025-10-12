namespace Live_Book.Infrastructure.Models.Sso;
public class GetProjectTokenResponseDto
{
	public string Title { get; set; }
	public string ClientId { get; set; }
	public string Token { get; set; }
	public DateTime ExpireDateTime { get; set; }
}
