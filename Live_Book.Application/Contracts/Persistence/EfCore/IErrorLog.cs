namespace Live_Book.Application.Contracts.Persistence.EfCore
{
	public interface IErrorLog : IAsyncDisposable
	{
		Task<string> Add(string message, string? innerMessage, string action);
	}
}
