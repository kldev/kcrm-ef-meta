using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using KCrm.Core.Definition;
using KCrm.Core.Entity.Users;
using KCrm.Core.Security;
using KCrm.Data.Context;
using KCrm.Logic.Core;
using KCrm.Logic.Security.Interfaces;
using KCrm.Logic.Services.Auth.Model;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Logic.Security {
    public class AuthenticateService : IAsyncDisposable, IAuthenticateService {
        private readonly AppUserContext _appUserContext;
        private readonly IAuthTokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthenticateService(AppUserContext appUserContext,
            IPasswordHasher passwordHasher,
            IAuthTokenService tokenService,
            IRefreshTokenService refreshTokenService) {
            _appUserContext = appUserContext;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        }

        private const string ErrorsPrefix = "Auth.Error";
        private ErrorDto UsernameOrPasswordInvalid => new ErrorDto { Message = "Username or password invalid", ErrorCode = $"{ErrorsPrefix}.UsernameOrPasswordInvalid" };

        public async Task<ResponseBase<AuthDto>> LoginAsync(string username, string password,  CancellationToken cancellationToken) {
            if (string.IsNullOrWhiteSpace (username) || string.IsNullOrWhiteSpace (password)) {
                return new ResponseBase<AuthDto> (UsernameOrPasswordInvalid);
            }

            var user = await _appUserContext.AppUsers.Where (x => x.Username == username && x.IsEnabled)
                .Include (x => x.UserRoles).ThenInclude (x => x.AppUserRole)
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

        private List<Claim> CreateClaims(User user) {
            var role = string.Empty;
            var isAdmin = user.UserRoles.Any (x => x.AppUserRole.Name == UserRoleTypes.Admin);
            var isRoot = user.UserRoles.Any (x => x.AppUserRole.Name == UserRoleTypes.Root);
            var isGuest = user.UserRoles.Count == 0;

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
                new Claim(AppUserIdentity.ClaimTypeUserId, user.Id.ToString("D")),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(AppUserIdentity.ClaimTypeIsAdmin, isAdmin.ToString()),
                new Claim(AppUserIdentity.ClaimTypeIsRoot, isRoot.ToString()),
                new Claim(AppUserIdentity.ClaimTypeIsGuest, isGuest.ToString()),
                new Claim(ClaimTypes.Role, role)
            };
        }

        public async Task<ResponseBase<AuthDto>> UseRefreshTokenAsync(string refreshToken,  CancellationToken cancellationToken) {

            var userId = await _refreshTokenService.UseAsync (refreshToken);

            var user = await _appUserContext.AppUsers.Include (x => x.UserRoles)
                .ThenInclude (x => x.AppUserRole)
                .SingleOrDefaultAsync (x => x.Id == userId && x.IsEnabled, cancellationToken);

            await _refreshTokenService.Revoke (refreshToken);

            return await CreateAuth (CreateClaims (user), userId, cancellationToken);

        }

        private async Task<ResponseBase<AuthDto>> CreateAuth(List<Claim> claims, Guid userId,  CancellationToken cancellationToken) {
            var token = _tokenService.GenerateToken (claims);

            var refreshToken = await _refreshTokenService.CreateAsync (userId);

            return new ResponseBase<AuthDto> (new AuthDto {
                Token = token,
                Role = claims.FirstOrDefault (x => x.Type == ClaimTypes.Role)?.Value,
                RefreshToken = refreshToken
            });
        }

        public async Task LogOutAsync(Guid userId,  CancellationToken cancellationToken) {
            var userIds = await _appUserContext.UserLogins.Where (x => x.UserId == userId).ToListAsync (cancellationToken );
            if (userIds.Any ( )) {
                _appUserContext.UserLogins.RemoveRange (userIds);
                await _appUserContext.SaveChangesAsync (cancellationToken );
            }
        }

        public async ValueTask DisposeAsync() {
            if (_appUserContext != null) {
                await _appUserContext.DisposeAsync ( );
            }
        }
    }
}
