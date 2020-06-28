using KCrm.Core;
using KCrm.Core.Entity.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Config.Users {
    public class UserLoginConfig : AppEntityTypeConfiguration<UserLogin> {
        protected override void ConfigureEntity(EntityTypeBuilder<UserLogin> builder) {
            builder.HasKey (x => x.Id);
            builder.HasIndex (x => x.UserId);
            builder.HasIndex (x => x.RefreshToken).IsUnique ( );
        }
    }
}
