using KCrm.Core;
using KCrm.Core.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Config.Users {
    public class AppUserRoleConfig : AppEntityTypeConfiguration<UserRole> {
        protected override void ConfigureEntity(EntityTypeBuilder<UserRole> builder) {
            builder.HasKey (x => x.Id);
            builder.Property (x => x.Description).HasMaxLength (512);
            builder.Property (x => x.Name).HasMaxLength (100).IsRequired ( );

            builder.HasMany (x => x.RoleUsers)
                .WithOne (x => x.AppUserRole)
                .HasForeignKey (x => x.UserRoleId)
                .OnDelete (DeleteBehavior.NoAction).IsRequired ( );
        }
    }
}
