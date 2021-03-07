using System;

namespace KCrm.Core.Entity.Users {
    public class UserHasRoleEntity : BaseGuidId {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }

        public UserAccountEntity User { get; set; }
        public UserRoleEntity RoleEntity { get; set; }
    }
}
