using System;

namespace KCrm.Core.Entity.Projects {
    public class ProjectHasTag : BaseId {
        public Guid TagId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
