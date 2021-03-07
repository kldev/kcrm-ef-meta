using System.Collections.Generic;

namespace KCrm.Core.Entity.Users {
    public class UserRoleEntity : BaseGuidId, ISoftDelete {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserHasRoleEntity> RoleUsers { get; set; }
    }
}
