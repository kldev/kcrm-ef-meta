using System;

namespace KCrm.Core.Entity.Projects {
    public class ProjectHasTagEntity : BaseGuidId {
        public Guid TagId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
