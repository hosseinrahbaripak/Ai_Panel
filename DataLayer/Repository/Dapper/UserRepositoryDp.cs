using System.Data;
using Dapper;
using Ai_Panel.Application.Contracts.Persistence.Dapper;
using Ai_Panel.Application.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Ai_Panel.Persistence.Repository.Dapper;

public class UserRepositoryDp : IUserRepositoryDp
{
    private readonly IDbConnection _db;
    public UserRepositoryDp(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("LiveBookConnection"));
    }
    public async Task<List<StatTimeBased>> GetUsersInstallationStas(string where = "", string join = "", object? parametrs = null)
    {
        var query = @"SELECT 
                        COALESCE(u.Year, us.Year) AS Year, 
                        COALESCE(u.Month, us.Month) AS Month,
                        COALESCE(u.Day, us.Day) AS Day,
                        COALESCE(u.Id, 0) AS Total,
                        COALESCE(us.Id, 0) AS Part
                    FROM 
                        (SELECT 
                            COUNT(DISTINCT u.UserId) AS Id,
                            DATEPART(YEAR, u.DateTime) AS Year, 
                            DATEPART(MONTH, u.DateTime) AS Month,
                            DATEPART(DAY, u.DateTime) AS Day
                        FROM Users u "+
                        join+
                        @"WHERE 
                        u.IsDelete = 0 
                        AND u.DateTime >= @DateTime 
                        AND u.DateTime <= @ToDateTime " +
                        where
                        + @"
                        GROUP BY 
                            DATEPART(YEAR, u.DateTime), 
                            DATEPART(MONTH, u.DateTime), 
                            DATEPART(DAY, u.DateTime)
                        ) u
                    FULL OUTER JOIN 
                        (SELECT 
                            COUNT(DISTINCT us.UserId) AS Id,
                            DATEPART(YEAR, us.DateTime) AS Year, 
                            DATEPART(MONTH, us.DateTime) AS Month,
                            DATEPART(DAY, us.DateTime) AS Day
                        FROM UserSessions us 
                        LEFT JOIN Users u ON us.UserId = u.UserId " +
                        join
                        + @"WHERE us.DateTime >= @DateTime 
                            AND us.DateTime <= @ToDateTime 
                            AND us.UserId NOT IN (
                                SELECT DISTINCT UserId FROM UserSessions WHERE DateTime < @DateTime
                            )" +
                        where
                        + @" GROUP BY 
                            DATEPART(YEAR, us.DateTime), 
                            DATEPART(MONTH, us.DateTime), 
                            DATEPART(DAY, us.DateTime) 
                        ) us
                    ON u.Year = us.Year 
                    AND u.Month = us.Month 
                    AND u.Day = us.Day;";
        var model = await _db.QueryAsync<StatTimeBased>(query, parametrs);
        return model.ToList();
    }

    public async Task<List<IdTitle>> GetUsersPer(string select = "", string where = "", string join = "", string groupBy = "", string orderBy = "", object? parameters = null)
    {
        var query =@"SELECT Count(*) as Id, " +
                    select +
					"FROM Users u  " +
                    join +
                    "WHERE u.IsDelete = 0 " +
                    where + 
                    "GROUP BY " +
                    groupBy +
                    "ORDER BY " +
                    orderBy;
        var model = await _db.QueryAsync<IdTitle>(query, parameters);
		return model.ToList();
    }
}