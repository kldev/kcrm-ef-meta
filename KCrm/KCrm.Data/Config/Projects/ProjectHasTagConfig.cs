using KCrm.Core;
using KCrm.Core.Entity.Projects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Config.Projects {
    public class ProjectHasTagConfig : AppEntityTypeConfiguration<ProjectHasTag> {
        protected override void ConfigureEntity(EntityTypeBuilder<ProjectHasTag> builder) {
            builder.HasKey (p => p.Id);
            builder.HasIndex (p => new {
                p.TagId,
                p.ProjectId
            }).IsUnique ( );
        }
    }
}
