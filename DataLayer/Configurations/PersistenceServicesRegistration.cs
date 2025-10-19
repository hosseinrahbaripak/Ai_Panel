
using Ai_Panel.Application.Contracts.Persistence.Dapper;
using Ai_Panel.Application.Contracts.Persistence.EfCore;
using Ai_Panel.Persistence.Repository.Dapper;
using Ai_Panel.Persistence.Repository.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ai_Panel.Persistence.Configurations;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AiPanelContext>(options =>
        {
            options.UseSqlServer(configuration
                .GetConnectionString("LiveBookConnection"));
        }, ServiceLifetime.Transient);

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        #region --IOC--EfCore
        services.AddScoped<IErrorLog, ErrorLogRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IApiRequestLog, ApiRequestLogRepository>();
        services.AddScoped<IUserAiChatLogsRepository, UserAiChatLogsRepository>();
        services.AddScoped<IAiConfigRepository, AiConfigRepository>();
        services.AddScoped<IAiModelRepository, AiModelRepository>();
        services.AddScoped<IAiPlatformRepository, AiPlatformRepository>();
        #endregion
        services.AddScoped<IUser, UserRepository>();
        #region --IOC--Dapper
        services.AddScoped<IUserRepositoryDp, UserRepositoryDp>();
        services.AddScoped<IUserAiChatLogsRepositoryDp, UserAiChatLogsRepositoryDp>();
        services.AddScoped<ITestAiConfigRepositoryDp, TestAiConfigRepositoryDp>();
        #endregion

        return services;
    }
}