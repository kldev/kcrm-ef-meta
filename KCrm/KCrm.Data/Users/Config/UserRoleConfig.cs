using KCrm.Core;
using KCrm.Core.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Users.Config {
    public class AppUserRoleConfig : AppEntityTypeConfiguration<UserRoleEntity> {
        protected override void ConfigureEntity(EntityTypeBuilder<UserRoleEntity> builder) {
            builder.HasKey (x => x.Id);
            builder.Property (x => x.Description).HasMaxLength (512);
            builder.Property (x => x.Name).HasMaxLength (100).IsRequired ( );

            builder.HasMany (x => x.RoleUsers)
                .WithOne (x => x.RoleEntity)
                .HasForeignKey (x => x.RoleId)
                .OnDelete (DeleteBehavior.NoAction).IsRequired ( );
        }
    }
}
