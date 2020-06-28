using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using KCrm.Core.Entity.Users;
using KCrm.Data.Context;
using KCrm.Logic.Security.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Logic.Security {
    public class RefreshTokenService : IAsyncDisposable, IRefreshTokenService {
        private AppUserContext _appUserContext;
        private TimeSpan _refreshTokenPeriod = TimeSpan.FromHours (4);

        public RefreshTokenService(AppUserContext appUserContext) {
            _appUserContext = appUserContext;
        }

        public async Task<string> CreateAsync(Guid userId) {

            var userLogins = await _appUserContext.UserLogins.Where (x => x.UserId == userId).ToListAsync ( );

            if (userLogins.Any ( )) {
                _appUserContext.UserLogins.RemoveRange (userLogins);
                await _appUserContext.SaveChangesAsync ( );
            }

            var refreshToken = Rng.Generate ( );
            await _appUserContext.UserLogins.AddAsync (new UserLogin {
                Id = Guid.NewGuid ( ),
                RefreshToken = refreshToken,
                UserId = userId,
                ValidPeriod = _refreshTokenPeriod
            });

            await _appUserContext.SaveChangesAsync ( );

            return refreshToken;
        }

        public async Task Revoke(string token) {
            if (string.IsNullOrEmpty (token)) {
                throw new ArgumentException ("Token can not be empty");
            }
            var userLogin = await _appUserContext.UserLogins.Where (x => x.RefreshToken == token).SingleOrDefaultAsync ( );
            if (userLogin != null) {
                _appUserContext.UserLogins.Remove (userLogin);
                await _appUserContext.SaveChangesAsync ( );
            }
        }

        public async Task<Guid> UseAsync(string refreshToken) {
            if (string.IsNullOrEmpty (refreshToken)) {
                throw new ArgumentException ("Token can not be empty");
            }

            var userLogin = await _appUserContext.UserLogins.Where (x => x.RefreshToken == refreshToken)
                .SingleOrDefaultAsync ( );
            if (userLogin == null) throw new ArgumentException ("Token can not be found");

            if (DateTime.UtcNow - userLogin.Created > _refreshTokenPeriod) {
                throw new ArgumentException ("Refresh token expired");
            }

            return userLogin.UserId;
        }

        public async ValueTask DisposeAsync() {
            if (_appUserContext != null) {
                await _appUserContext.DisposeAsync ( );
            }
        }
        private static class Rng {
            private static readonly string[] s_specialChars = new[] { "/", "\\", "=", "+", "?", ":", "&" };

            public static string Generate(int length = 50, bool removeSpecialChars = true) {
                using (var rng = new RNGCryptoServiceProvider ( )) {
                    var bytes = new byte[length];
                    rng.GetBytes (bytes);
                    var result = Convert.ToBase64String (bytes);

                    return removeSpecialChars
                        ? s_specialChars.Aggregate (result, (current, chars) => current.Replace (chars, string.Empty))
                        : result;
                }
            }
        }
    }
}
