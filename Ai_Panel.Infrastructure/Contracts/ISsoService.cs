namespace Ai_Panel.Infrastructure.Contracts;
public interface ISsoService
{
	Task<string> GetToken();
	Task<bool> ValidateToken(string token);
}
