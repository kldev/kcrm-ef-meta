using AutoMapper;
using KCrm.Core.Security;
using KCrm.Logic.Services.Projects;
using KCrm.Server.Api.BackgroundService;
using KCrm.Server.Api.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KCrm.Server.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddSeedSetup (Configuration);
            services.AddHttpContextAccessor ( );
            services.AddAutoMapper (typeof (ProjectMappingProfile));
            services.AddCrmDatabases (Configuration);
            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher> ( );
            services.AddScuttorServices ( );
            services.AddAppMediatR ( );

            services.AddControllers ( );
            services.AddAppCors (Configuration);
            services.AddSwaggerService ( );
            services.AddJwt (Configuration);
            services.AddS3Minio (Configuration);
            
            services.AddAppHealthCheck (Configuration);
            services.AddHostedService<DbMigrateService> ( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ( )) {
                app.UseDeveloperExceptionPage ( );
            }

            app.UseAppCors ( );
            app.UseAppMiddleware ( );

            //            app.UseHttpsRedirection ( );

            app.AddSwaggerApplication ( );

            app.UseRouting ( );

            app.UseAuthentication ( );
            app.UseAuthorization ( );

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ( );
                endpoints.MapHealthChecks ("/healthz", new HealthCheckOptions ( ) {

                });
            });
        }
    }
}
