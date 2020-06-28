using KCrm.Core;
using KCrm.Core.Entity.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Config.Common {
    public class CountryConfig : AppEntityTypeConfiguration<Country> {
        protected override void ConfigureEntity(EntityTypeBuilder<Country> builder) {
            builder.Property (x => x.Name).HasMaxLength (100).IsRequired ( );
            builder.Property (x => x.Code).HasMaxLength (4).IsRequired ( );
            builder.Property (x => x.Iso).HasMaxLength (4).IsRequired ( );

            builder.HasIndex (x => x.Name).IsUnique ( );
            builder.HasIndex (x => x.Iso).IsUnique ( );
        }
    }
}
