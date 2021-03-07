using KCrm.Core;
using KCrm.Core.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Users.Config {
    public class UserHasRoleConfig : AppEntityTypeConfiguration<UserHasRoleEntity> {
        protected override void ConfigureEntity(EntityTypeBuilder<UserHasRoleEntity> builder) {
            builder.HasKey (x => x.Id);
            builder.HasIndex (x => new { x.UserId, x.RoleId }).IsUnique ( );

            builder.HasOne (x => x.User)
                .WithMany (x => x.UserRoles).HasForeignKey (x => x.UserId).OnDelete (DeleteBehavior.Cascade).IsRequired ( );

            builder.HasOne (x => x.RoleEntity)
                .WithMany (x => x.RoleUsers).HasForeignKey (x => x.RoleId).OnDelete (DeleteBehavior.Cascade).IsRequired ( );
        }
    }
}
