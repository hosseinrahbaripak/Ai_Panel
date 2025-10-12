using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Ai_Panel.Application.Configurations
{
    public static class ApplicationServicesRegistration
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        { 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
