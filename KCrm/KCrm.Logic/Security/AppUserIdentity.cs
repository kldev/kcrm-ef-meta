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
        public Guid UserId { get; set; }
        public string UserRole { get; set; }
        private IEnumerable<Claim> Claims { get; set; } = new List<Claim> ( );

        public AppUserIdentity(ClaimsPrincipal claimsPrincipal) {
            if (claimsPrincipal.Claims.Any ( )) {
                Claims = claimsPrincipal.Claims;
                UserId = Guid.Parse (Claims?.FirstOrDefault (x => x.Type == ClaimTypeUserId)?.Value ?? "");
                UserRole = Claims?.FirstOrDefault (x => x.Type == ClaimTypes.Role)?.Value ?? "";
            }
        }


    }
}
