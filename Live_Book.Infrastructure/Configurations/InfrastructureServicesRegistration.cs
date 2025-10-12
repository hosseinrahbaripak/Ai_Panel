using Ai_Panel.Infrastructure.Contracts;
using Ai_Panel.Infrastructure.Contracts.Notification;
using Ai_Panel.Infrastructure.Services;
using Ai_Panel.Infrastructure.Services.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace Ai_Panel.Infrastructure.Configurations;

public static class InfrastructureServicesRegistration
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<ISmsManagerService, SmsManagerService>();
        services.AddScoped<ISsoService, SsoService>();
    }
}