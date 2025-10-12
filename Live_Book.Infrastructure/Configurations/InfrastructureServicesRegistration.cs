using Live_Book.Infrastructure.Contracts;
using Live_Book.Infrastructure.Contracts.Notification;
using Live_Book.Infrastructure.Services;
using Live_Book.Infrastructure.Services.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace Live_Book.Infrastructure.Configurations;

public static class InfrastructureServicesRegistration
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<ISmsManagerService, SmsManagerService>();
        services.AddScoped<ISsoService, SsoService>();
    }
}