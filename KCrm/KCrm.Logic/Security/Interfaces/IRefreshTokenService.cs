using System;
using System.Threading.Tasks;

namespace KCrm.Logic.Security.Interfaces {
    public interface IRefreshTokenService {
        Task<string> CreateAsync(Guid userId);
        Task Revoke(string token);
        Task<Guid> UseAsync(string refreshToken);
    }
}
