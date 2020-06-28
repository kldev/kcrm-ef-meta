using System;
using System.Threading;
using System.Threading.Tasks;
using KCrm.Logic.Core;
using KCrm.Logic.Services.Auth.Model;

namespace KCrm.Logic.Security.Interfaces {
    public interface IAuthenticateService {
        Task<ResponseBase<AuthDto>> LoginAsync(string username, string password,  CancellationToken cancellationToken);
        Task<ResponseBase<AuthDto>> UseRefreshTokenAsync(string refreshToken,  CancellationToken cancellationToken);
        Task LogOutAsync(Guid userId,  CancellationToken cancellationToken);
    }
}
