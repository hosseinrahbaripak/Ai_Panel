using Ai_Panel.Domain;

namespace Ai_Panel.Application.Contracts.Persistence.EfCore
{
	public interface IAboutUs : IAsyncDisposable
	{
		Task<List<AboutUs>> GetAboutUs();
		Task<bool> IsExistAboutUs();
		Task AddAboutUs(AboutUs aboutUs);
		Task EditAboutUs(AboutUs aboutUs);
		Task<AboutUs> FindAboutUs(int id);
	}
}
