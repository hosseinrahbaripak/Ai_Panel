namespace MHelliClass.Infrastructure.Model.Sso;

public class ValidateTokenRequestDto
{
    public string Title { get; set; }
    public string ClientId { get; set; }
    public string ApiKey { get; set; }
    public string SecretKey { get; set; }
    public string Token { get; set; }
}