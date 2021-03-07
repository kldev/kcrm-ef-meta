using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using KCrm.Core.Definition;
using KCrm.Core.Entity.Users;
using KCrm.Core.Security;
using KCrm.Data.Users;
using KCrm.Logic.Core;
using KCrm.Logic.Security.Interfaces;
using KCrm.Logic.Services.Auth.Model;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Logic.Security {
    public class AuthenticateService : IAsyncDisposable, IAuthenticateService {
        private readonly AppUserContext _appUserContext;
        private readonly IAuthTokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticateService(AppUserContext appUserContext,
            IPasswordHasher passwordHasher,
            IAuthTokenService tokenService
        ) {
            _appUserContext = appUserContext;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        private const string ErrorsPrefix = "Auth.Error";
        private ErrorDto UsernameOrPasswordInvalid => new ErrorDto { Message = "Username or password invalid", ErrorCode = $"{ErrorsPrefix}.UsernameOrPasswordInvalid" };

        public async Task<ResponseBase<AuthDto>> LoginAsync(string username, string password,  CancellationToken cancellationToken) {
            if (string.IsNullOrWhiteSpace (username) || string.IsNullOrWhiteSpace (password)) {
                return new ResponseBase<AuthDto> (UsernameOrPasswordInvalid);
            }

            var user = await _appUserContext.UserAccounts.Where (x => x.Username == username && x.IsEnabled)
                .Include (x => x.UserRoles).ThenInclude (x => x.RoleEntity)
                .AsNoTracking ( )
                .SingleOrDefaultAsync (cancellationToken );

            if (user == null) {
                return new ResponseBase<AuthDto> (UsernameOrPasswordInvalid);
            }

            if (!_passwordHasher.Match (password, user.Password)) {
                return new ResponseBase<AuthDto> (UsernameOrPasswordInvalid);
            }

            var claims = CreateClaims (user);

            return await CreateAuth (claims, user.Id, cancellationToken);

        }

        private List<Claim> CreateClaims(UserAccountEntity userAccountEntity) {
            var role = string.Empty;
            var isAdmin = userAccountEntity.UserRoles.Any (x => x.RoleEntity.Name == UserRoleTypes.Admin);
            var isRoot = userAccountEntity.UserRoles.Any (x => x.RoleEntity.Name == UserRoleTypes.Root);
            var isGuest = userAccountEntity.UserRoles.Count == 0;

            if (isRoot) {
                role = UserRoleTypes.Root;
            }
            else if (isAdmin) {
                role = UserRoleTypes.Admin;
            }
            else if (isGuest) {
                role = UserRoleTypes.Guest;
            }

            return new List<Claim>
            {
                new Claim(AppUserIdentity.ClaimTypeUserId, userAccountEntity.Id.ToString("D")),
                new Claim(ClaimTypes.Name, userAccountEntity.Username),
                new Claim(AppUserIdentity.ClaimTypeIsAdmin, isAdmin.ToString()),
                new Claim(AppUserIdentity.ClaimTypeIsRoot, isRoot.ToString()),
                new Claim(AppUserIdentity.ClaimTypeIsGuest, isGuest.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(AppUserIdentity.ClaimTypeFullName, userAccountEntity.Name + " " + userAccountEntity.LastName )
            };
        }

       
        private async Task<ResponseBase<AuthDto>> CreateAuth(List<Claim> claims, Guid userId,  CancellationToken cancellationToken) {
            var token = _tokenService.GenerateToken (claims);
            var user = await _appUserContext.UserAccounts.FirstAsync (x => x.Id == userId, cancellationToken);
            
            return new ResponseBase<AuthDto> (new AuthDto {
                Token = token,
                Role = claims.FirstOrDefault (x => x.Type == ClaimTypes.Role)?.Value,
                FullName = (user.Name + " " + user.LastName).Trim(), Username = user.Username
            });
        }

        public Task LogOutAsync(Guid userId,  CancellationToken cancellationToken) {
           // nothing
           return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync() {
            if (_appUserContext != null) {
                await _appUserContext.DisposeAsync ( );
            }
        }
    }
}
