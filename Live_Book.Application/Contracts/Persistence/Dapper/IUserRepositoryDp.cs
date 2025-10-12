using Live_Book.Application.DTOs;

namespace Live_Book.Application.Contracts.Persistence.Dapper;

public interface IUserRepositoryDp
{
	Task<List<IdTitle>> GetUsersPer(string select = "", string where = "", string join = "", string groupBy = "", string orderBy = "", object? parameters = null);
	Task<List<StatTimeBased>> GetUsersInstallationStas(string where = "", string join="", object? parametrs = null);
}