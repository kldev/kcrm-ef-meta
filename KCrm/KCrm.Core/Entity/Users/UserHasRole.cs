using System;

namespace KCrm.Core.Entity.Users {
    public class UserHasRole : BaseId {
        public Guid UserRoleId { get; set; }
        public Guid UserId { get; set; }

        public virtual User AppUser { get; set; }
        public virtual UserRole AppUserRole { get; set; }
    }
}
