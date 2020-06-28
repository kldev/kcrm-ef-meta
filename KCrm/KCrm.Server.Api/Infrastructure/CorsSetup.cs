using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KCrm.Server.Api.Infrastructure {
    public static class CorsSetup {

        const string PolicyName = "corsPolicy";

        public static IServiceCollection AddAppCors(this IServiceCollection services, IConfiguration configuration) {
            var cors = configuration["Cors"] ?? "http://localhost:4200";

            var allowedOrigins = cors.Split (new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);

            services.AddCors (options => {
                options.AddPolicy (PolicyName, config =>
                    config.WithOrigins (allowedOrigins)
                        .AllowAnyMethod ( )
                        .AllowAnyHeader ( )
                        .AllowCredentials ( ));
            });

            return services;
        }

        public static IApplicationBuilder UseAppCors(this IApplicationBuilder app) {
            app.UseCors (PolicyName);
            return app;
        }
    }
}
