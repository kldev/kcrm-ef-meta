using KCrm.Core;
using KCrm.Core.Entity.Projects;
using KCrm.Core.Entity.Projects.Definitions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KCrm.Data.Projects.Config {
    public class ProjectHasUserConfig : AppEntityTypeConfiguration<ProjectHasUserEntity> {
        protected override void ConfigureEntity(EntityTypeBuilder<ProjectHasUserEntity> builder) {

            builder.HasKey (x => x.Id);
            builder.HasIndex (x => new { x.ProjectId, x.UserId }).IsUnique ( );

            var userRoleTypeConverter = new EnumToStringConverter<UserRoleInProjectType> ( );
            builder.Property (x => x.UserRoleType).HasConversion (userRoleTypeConverter);
        }
    }
}
