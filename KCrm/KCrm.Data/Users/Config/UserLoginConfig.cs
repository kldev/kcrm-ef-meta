using KCrm.Core;
using KCrm.Core.Entity.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Users.Config {
    public class UserLoginConfig : AppEntityTypeConfiguration<UserLoginActivityEntity> {
        protected override void ConfigureEntity(EntityTypeBuilder<UserLoginActivityEntity> builder) {
            builder.HasKey (x => x.Id);
            builder.HasIndex (x => x.UserId);
            builder.HasIndex (x => x.RefreshToken).IsUnique ( );
        }
    }
}
