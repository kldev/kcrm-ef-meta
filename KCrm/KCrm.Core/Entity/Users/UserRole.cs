using System.Collections.Generic;

namespace KCrm.Core.Entity.Users {
    public class UserRole : BaseId, ISoftDelete {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserHasRole> RoleUsers { get; set; }
    }
}
