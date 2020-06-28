using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace KCrm.Server.Api.Infrastructure {
    public static class SwaggerSetup {

        public static IServiceCollection AddSwaggerService(this IServiceCollection services) {
            services.AddSwaggerGen (config => {
                config.SwaggerDoc ("v1", new OpenApiInfo { Title = "K-CRM API", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder AddSwaggerApplication(this IApplicationBuilder app) {
            app.UseSwagger ( );
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "K-CRM API v1");
                c.RoutePrefix = "docs";
                c.DocumentTitle = "Docs";
            });
            return app;
        }
    }
}
