using KCrm.Core;
using KCrm.Core.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Users.Config {
    public class UserConfig : AppEntityTypeConfiguration<UserAccountEntity> {
        protected override void ConfigureEntity(EntityTypeBuilder<UserAccountEntity> builder) {
            builder.HasKey (x => x.Id);
            builder.Property (x => x.Email).HasMaxLength (100).IsRequired ( );
            builder.Property (x => x.Password).HasMaxLength (100).IsRequired ( );
            builder.Property (x => x.Provider).HasDefaultValue ("crm").HasMaxLength (100).IsRequired ( );
            builder.Property (x => x.ProviderId).HasMaxLength (100);
            builder.Property (x => x.Username).HasMaxLength (100).IsRequired ( );
            builder.Property (x => x.LastName).HasMaxLength (100).IsRequired ( );
            builder.Property (x => x.Name).HasMaxLength (100).IsRequired ( );
            builder.Property (x => x.IsEnabled).IsRequired ( );

            builder.HasMany (x => x.UserRoles)
                .WithOne (x => x.User).HasForeignKey (x => x.UserId)
                .OnDelete (DeleteBehavior.NoAction);
        }
    }
}
