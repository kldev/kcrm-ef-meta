using System;
using KCrm.Core.Entity.Projects.Definitions;

namespace KCrm.Core.Entity.Projects {
    public class ProjectHasUserEntity : BaseGuidId {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public UserRoleInProjectType UserRoleType { get; set; }
    }
}
