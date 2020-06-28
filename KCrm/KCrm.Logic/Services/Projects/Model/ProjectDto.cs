using System;
using KCrm.Core.Entity.Projects;

namespace KCrm.Logic.Services.Projects.Model {
    public class ProjectDto {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public ProjectType ProjectType { get; set; } = ProjectType.FinTech;
        public DateTime? StartDateTimeUtc { get; set; }
        public DateTime? PlanedEnDateTimeUtc { get; set; }
        public DateTime? EndDateTimeUtc { get; set; }
    }
}
