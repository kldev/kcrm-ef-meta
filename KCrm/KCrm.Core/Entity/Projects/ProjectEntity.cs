using System;
using KCrm.Core.Entity.Projects.Definitions;

namespace KCrm.Core.Entity.Projects {
    public class ProjectEntity : BaseGuidId, ISoftDelete, IChange {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public ProjectType ProjectType { get; set; } = ProjectType.FinTech;
        public DateTime? StartDateTimeUtc { get; set; }
        public DateTime? PlanedEndDateTimeUtc { get; set; }
        public DateTime? EndDateTimeUtc { get; set; }
    }
}
