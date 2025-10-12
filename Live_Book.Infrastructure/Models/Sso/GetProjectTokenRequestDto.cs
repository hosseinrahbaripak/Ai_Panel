namespace Ai_Panel.Infrastructure.Models.Sso;
public class GetProjectTokenRequestDto
{
	public string Title { get; set; }
	public string ClientId { get; set; }
	public string ApiKey { get; set; }
	public string SecretKey { get; set; }
}
