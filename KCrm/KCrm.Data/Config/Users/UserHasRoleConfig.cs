using KCrm.Core;
using KCrm.Core.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Config.Users {
    public class UserHasRoleConfig : AppEntityTypeConfiguration<UserHasRole> {
        protected override void ConfigureEntity(EntityTypeBuilder<UserHasRole> builder) {
            builder.HasKey (x => x.Id);
            builder.HasIndex (x => new { x.UserId, x.UserRoleId }).IsUnique ( );

            builder.HasOne (x => x.AppUser)
                .WithMany (x => x.UserRoles).HasForeignKey (x => x.UserId).OnDelete (DeleteBehavior.Cascade).IsRequired ( );

            builder.HasOne (x => x.AppUserRole)
                .WithMany (x => x.RoleUsers).HasForeignKey (x => x.UserRoleId).OnDelete (DeleteBehavior.Cascade).IsRequired ( );

        }
    }
}
