using KCrm.Core;
using KCrm.Core.Entity.GeoLocation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.GeoLocation.Config {
    public class CountryConfig : AppEntityTypeConfiguration<CountryEntity> {
        protected override void ConfigureEntity(EntityTypeBuilder<CountryEntity> builder) {
            builder.Property (x => x.Name).HasMaxLength (100).IsRequired ( );
            builder.Property (x => x.Code).HasMaxLength (4).IsRequired ( );
            builder.Property (x => x.Iso).HasMaxLength (4).IsRequired ( );

            builder.HasIndex (x => x.Name).IsUnique ( );
            builder.HasIndex (x => x.Iso).IsUnique ( );
        }
    }
}
