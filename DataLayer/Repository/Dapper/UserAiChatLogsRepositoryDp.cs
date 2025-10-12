using System.Data;
using Dapper;
using Live_Book.Application.Contracts.Persistence.Dapper;
using Live_Book.Application.DTOs;
using Live_Book.Application.DTOs.AiChat;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Live_Book.Persistence.Repository.Dapper;
public class UserAiChatLogsRepositoryDp : IUserAiChatLogsRepositoryDp
{
    private readonly IDbConnection _db;
    public UserAiChatLogsRepositoryDp(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("LiveBookConnection"));
    }
    public async Task<List<WordsCountIdTitle>> GetAiChatWordsCount(object parameters)
    {
        var query =
            @"SELECT STRING_AGG(CONCAT(uacl.UserMessage, ' ', uacl.AiResponse), ' ') AS IdString, " +
            "DATEPART(YEAR, uacl.DateTime) as Year, " +
            "DATEPART(MONTH , uacl.DateTime) as Month, " +
            "DATEPART(DAY, uacl.DateTime) as Day " +
            "FROM UserAiChatLogs uacl  " +
                "WHERE " +
                "uacl.DateTime >= @DateTime AND " +
                "uacl.DateTime <= @ToDateTime " +
            "GROUP BY " +
                "DATEPART(YEAR, uacl.DateTime), " +
                "DATEPART(MONTH , uacl.DateTime), " +
                "DATEPART(DAY, uacl.DateTime) " +
            "ORDER BY " +
                "DATEPART(YEAR, uacl.DateTime), " +
                "DATEPART(MONTH , uacl.DateTime), " +
                "DATEPART(DAY, uacl.DateTime) ";

        var model = await _db.QueryAsync<WordsCountIdTitle>(query.ToString(), parameters);
        return model.ToList();
    }
    public async Task<List<IdTitleTimeBased>> GetAiChatsCount(object parameters)
    {
        var query = @"SELECT Count(*) as Id, " +
                    "DATEPART(YEAR, uacl.DateTime) as Year, " +
                    "DATEPART(MONTH , uacl.DateTime) as Month, " +
                    "DATEPART(DAY, uacl.DateTime) as Day " +
                    "FROM UserAiChatLogs uacl " +
                    "WHERE " +
                        "uacl.DateTime >= @DateTime AND " +
                        "uacl.DateTime <= @ToDateTime " +
                    "GROUP BY " +
                        "DATEPART(YEAR, uacl.DateTime), " +
                        "DATEPART(MONTH , uacl.DateTime), " +
                        "DATEPART(DAY, uacl.DateTime) " +
                    "ORDER BY " +
                        "DATEPART(YEAR, uacl.DateTime), " +
                        "DATEPART(MONTH , uacl.DateTime), " +
                        "DATEPART(DAY, uacl.DateTime) ";


        var model = await _db.QueryAsync<IdTitleTimeBased>(query, parameters);
        return model.ToList();
    }
    public async Task<List<AiChatLogDto>> GetAiChatlogs(string where, string orderBy, object parameters)
    {
        var query = @"SELECT " +
            "u.Name as Name, b.Name as Book, bp.PartName as Part, " +
            "uacl.UserMessage as UserMessage, uacl.AiResponse as AiResponse, " +
            "uacl.DateTime as Date, uacl.AiCouldResponse, " +
            "(uacl.SummarizationCost + uacl.RequestCost + uacl.EmbeddingCost) as Cost " +
            ", ac.Version , am.title As AiModel , am2.Title As Ai " +
            "FROM UserAiChatLogs uacl " +
            "INNER JOIN Users u ON uacl.UserId = u.UserId " +
            "INNER JOIN Books b ON uacl.BookId = b.Id " +
            "INNER JOIN BookParts bp ON uacl.PartId = bp.PartId " +
            "LEFT OUTER JOIN AiConfigs ac on uacl.AiConfigId = ac.Id  " +
            "LEFT OUTER JOIN AiModels am on ac.AiModelId = am.Id  " +
            "LEFT OUTER JOIN AiModels am2 on am.ParentId = am2.Id " +
            "WHERE u.IsDelete = 0 ANd b.IsDelete = 0 " +
            where +
            orderBy;
        var model = await _db.QueryAsync<AiChatLogDto>(query, parameters);
        return model.ToList();
    }
}
