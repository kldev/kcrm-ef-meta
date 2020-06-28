using System.Reflection;
using KCrm.Core.Security;
using KCrm.Logic.Security.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KCrm.Server.Api.Infrastructure {
    public static class ScuttorConfig {
        public static IServiceCollection AddScuttorServices(this IServiceCollection services) {
            services.Scan (scan => {
                scan.FromAssemblies (Assembly.GetAssembly (typeof (IPasswordHasher))!)
                    .AddClasses ( ).AsMatchingInterface ( ).WithScopedLifetime ( );

                scan.FromAssemblies (Assembly.GetAssembly (typeof (IAuthenticateService))!)
                    .AddClasses ( ).AsMatchingInterface ( ).WithScopedLifetime ( );
            });
            return services;
        }
    }
}
