using KCrm.Data.Aegis;
using KCrm.Data.GeoLocation;
using KCrm.Data.Projects;
using KCrm.Data.Tags;
using KCrm.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KCrm.Server.Api.Infrastructure {
    public static class DatabaseConfig {
        private static ILoggerFactory CreateLogger() {
            var loggerFactory = LoggerFactory.Create (builder => {
                builder.AddFilter ("Microsoft", LogLevel.Warning)
                    .AddFilter ("System", LogLevel.Warning)
                    .AddFilter (DbLoggerCategory.Name, LogLevel.Information)
                    .AddConsole ( );
            }
            );

            return loggerFactory;
        }

        public static IServiceCollection AddCrmDatabases(this IServiceCollection services, IConfiguration configuration) {
            Setup<ProjectContext> (services, configuration);
            Setup<TagContext> (services, configuration);
            SetupAegis (services, configuration);
            Setup<AppUserContext> (services, configuration);
            Setup<GeoLocationContext> (services, configuration);
            return services;
        }

        private static void Setup<T>(IServiceCollection services, IConfiguration configuration) where T : DbContext {
            services.AddDbContext<T> (options => {
                options.UseNpgsql (configuration[$"ConnectionStrings:AppConnection"],
                    b => b.MigrationsAssembly (typeof (T).Assembly.FullName));

                options.UseSnakeCaseNamingConvention ( );
#if DEBUG
                options.UseLoggerFactory (CreateLogger ( ));
#endif
            });
        }

        private static void SetupAegis(IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<AegisContext> (options => {
                options.UseNpgsql (configuration["ConnectionStrings:AegisConnection"]);
#if DEBUG
                options.UseLoggerFactory (CreateLogger ( ));
#endif
            });

        }
    }
}
