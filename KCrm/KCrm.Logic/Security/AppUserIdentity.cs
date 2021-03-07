using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace KCrm.Logic.Security {
    public class AppUserIdentity {

        public const string ClaimTypeUserId = "UserId";
        public const string ClaimTypeIsAdmin = "IsAdmin";
        public const string ClaimTypeIsRoot = "IsRoot";
        public const string ClaimTypeIsGuest = "IsGuest";
        public const string ClaimTypeFullName = "FullName";
        public Guid UserId { get; private set; }
        public string UserRole { get; private set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        private IEnumerable<Claim> Claims { get; set; } = new List<Claim> ( );

        public AppUserIdentity(ClaimsPrincipal claimsPrincipal) {
            if (claimsPrincipal.Claims.Any ( )) {
                Claims = claimsPrincipal.Claims;
                UserId = Guid.Parse (Claims?.FirstOrDefault (x => x.Type == ClaimTypeUserId)?.Value ?? "");
                UserRole = Claims?.FirstOrDefault (x => x.Type == ClaimTypes.Role)?.Value ?? "";
                FullName = Claims?.FirstOrDefault (x => x.Type == AppUserIdentity.ClaimTypeFullName)?.Value ?? "";
                Username = Claims?.FirstOrDefault (x => x.Type == ClaimTypes.Name)?.Value ?? "";
            }
        }


    }
}
