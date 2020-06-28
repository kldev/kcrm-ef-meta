using System.Collections.Generic;
using System.Security.Claims;

namespace KCrm.Logic.Security.Interfaces {
    public interface IAuthTokenService {
        string GenerateToken(List<Claim> claims);
    }
}
