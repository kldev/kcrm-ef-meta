using System;
using System.Security.Claims;
using System.Text;
using KCrm.Core.Definition;
using KCrm.Logic.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace KCrm.Server.Api.Infrastructure {
    public static class JwtConfig {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration) {

            var auth = new AuthConfig {
                Secret = configuration[$"{nameof (AuthConfig)}:{nameof (AuthConfig.Secret)}"]
            };

            services.AddAuthentication (x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (auth.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization (options => {
                options.AddPolicy (UserRoleTypes.Root, policy => policy.RequireClaim (ClaimTypes.Role, UserRoleTypes.Root));
                options.AddPolicy (UserRoleTypes.Admin, policy => policy.RequireClaim (ClaimTypes.Role, UserRoleTypes.Admin));
                options.AddPolicy (UserRoleTypes.Seller, policy => policy.RequireClaim (ClaimTypes.Role, UserRoleTypes.Seller));
            });

            return services;
        }
    }
}
