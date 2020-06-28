using System;

namespace KCrm.Core.Entity.Projects {
    public class ProjectHasUser : BaseId {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public UserRoleInProjectType UserRoleType { get; set; }
    }
}
