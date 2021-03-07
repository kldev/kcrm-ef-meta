using KCrm.Core;
using KCrm.Core.Entity.Projects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Projects.Config {
    public class ProjectHasTagConfig : AppEntityTypeConfiguration<ProjectHasTagEntity> {
        protected override void ConfigureEntity(EntityTypeBuilder<ProjectHasTagEntity> builder) {
            builder.HasKey (p => p.Id);
            builder.HasIndex (p => new {
                p.TagId,
                p.ProjectId
            }).IsUnique ( );
        }
    }
}
