using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KCrm.Server.Api.Infrastructure {
    public static class HealthChecksSetup {
        public static void AddAppHealthCheck(this IServiceCollection services, IConfiguration configuration) {
            services.AddHealthChecks ( )
                .AddNpgSql (configuration[$"ConnectionStrings:AppConnection"]);
        }

    }
}
