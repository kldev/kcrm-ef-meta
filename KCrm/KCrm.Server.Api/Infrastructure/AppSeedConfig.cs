using KCrm.Server.Api.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KCrm.Server.Api.Infrastructure {
    public static class AppSeedConfig {
        
        public static IServiceCollection AddSeedSetup(this IServiceCollection services, IConfiguration configuration) {

            services.Configure<AppSeedOptions> (configuration.GetSection (nameof(AppSeedOptions)));
            return services;
        }

    }
}
