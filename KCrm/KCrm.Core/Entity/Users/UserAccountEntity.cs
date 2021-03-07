using System.Collections.Generic;

namespace KCrm.Core.Entity.Users {
    public class UserAccountEntity : BaseGuidId, ISoftDelete, IChange {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }
        public string Provider { get; set; }
        public string ProviderId { get; set; }
        public ICollection<UserHasRoleEntity> UserRoles { get; set; } = new List<UserHasRoleEntity> ( );
    }
}
