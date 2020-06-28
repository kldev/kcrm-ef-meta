using System;

namespace KCrm.Data.Aegis.Entity {
    public partial class UserAndRoles {
        public Guid? Id { get; set; }
        public Guid? UserRoleId { get; set; }
        public Guid? UserId { get; set; }
        public string Roles { get; set; }
        public string Topusers { get; set; }
    }
}
