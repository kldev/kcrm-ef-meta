using KCrm.Core;
using KCrm.Core.Entity.Projects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KCrm.Data.Config.Projects {
    public class ProjectHasUserConfig : AppEntityTypeConfiguration<ProjectHasUser> {
        protected override void ConfigureEntity(EntityTypeBuilder<ProjectHasUser> builder) {

            builder.HasKey (x => x.Id);
            builder.HasIndex (x => new { x.ProjectId, x.UserId }).IsUnique ( );

            var userRoleTypeConverter = new EnumToStringConverter<UserRoleInProjectType> ( );
            builder.Property (x => x.UserRoleType).HasConversion (userRoleTypeConverter);
        }
    }
}
