namespace Live_Book.Infrastructure.Contracts;
public interface ISsoService
{
	Task<string> GetToken();
	Task<bool> ValidateToken(string token);
}
