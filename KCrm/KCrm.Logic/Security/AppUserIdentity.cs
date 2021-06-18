using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace KCrm.Logic.Security {

    public static class AppUserIdentityClaimNames {
        public const string ClaimTypeUserId = "UserId";
        public const string ClaimTypeIsAdmin = "IsAdmin";
        public const string ClaimTypeIsRoot = "IsRoot";
        public const string ClaimTypeIsGuest = "IsGuest";
        public const string ClaimTypeFullName = "FullName";
        public const string ClaimAvatarId = "AvatarId";
    }
    
    public class AppUserIdentity {
        public Guid UserId { get; private set; }
        public string UserRole { get; private set; }
        public string FullName { get; private set; }
        public string Username { get; private set; }
        public string AvatarId { get; private set; }
        private IEnumerable<Claim> Claims { get; set; } = new List<Claim> ( );

        public AppUserIdentity(ClaimsPrincipal claimsPrincipal) {
            if (claimsPrincipal.Claims.Any ( )) {
                Claims = claimsPrincipal.Claims;
                UserId = Guid.Parse (Claims?.FirstOrDefault (x => x.Type == AppUserIdentityClaimNames.ClaimTypeUserId)?.Value ?? "");
                UserRole = Claims?.FirstOrDefault (x => x.Type == ClaimTypes.Role)?.Value ?? "";
                FullName = Claims?.FirstOrDefault (x => x.Type == AppUserIdentityClaimNames.ClaimTypeFullName)?.Value ?? "";
                Username = Claims?.FirstOrDefault (x => x.Type == ClaimTypes.Name)?.Value ?? "";
                AvatarId = Claims?.FirstOrDefault (x => x.Type == AppUserIdentityClaimNames.ClaimAvatarId)?.Value ?? "";
            }
        }
    }
}
