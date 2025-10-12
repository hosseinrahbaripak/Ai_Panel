namespace Ai_Panel.Application.Contracts.Persistence.EfCore
{
	public interface IErrorLog : IAsyncDisposable
	{
		Task<string> Add(string message, string? innerMessage, string action);
	}
}
