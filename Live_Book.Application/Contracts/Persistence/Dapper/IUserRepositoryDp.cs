using Ai_Panel.Application.DTOs;

namespace Ai_Panel.Application.Contracts.Persistence.Dapper;

public interface IUserRepositoryDp
{
	Task<List<IdTitle>> GetUsersPer(string select = "", string where = "", string join = "", string groupBy = "", string orderBy = "", object? parameters = null);
	Task<List<StatTimeBased>> GetUsersInstallationStas(string where = "", string join="", object? parametrs = null);
}