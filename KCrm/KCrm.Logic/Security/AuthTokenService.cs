using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KCrm.Logic.Config;
using KCrm.Logic.Security.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KCrm.Logic.Security {
    public class AuthTokenService : IAuthTokenService {
        private AuthConfig Config { get; set; }
        public AuthTokenService(IConfiguration configuration) {
            Config = new AuthConfig {
                Secret = configuration[$"{nameof (AuthConfig)}:{nameof (AuthConfig.Secret)}"]
            };

            if (!Config.IsValid) {
                throw new ArgumentException ("AuthConfig is invalid");
            }
        }

        public string GenerateToken(List<Claim> claims) {
            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (Config.Secret));
            var credentials = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken (
                "self",
                null,
                claims,
                expires: DateTime.UtcNow.AddDays (1),
                signingCredentials: credentials
            );
            var token = new JwtSecurityTokenHandler ( ).WriteToken (jwtToken);
            return token;
        }
    }
}
