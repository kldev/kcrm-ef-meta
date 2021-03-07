using KCrm.Core;
using KCrm.Core.Entity.Tags;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Tags.Config {
    public class TagGroupConfig : AppEntityTypeConfiguration<TagGroupEntity> {
        protected override void ConfigureEntity(EntityTypeBuilder<TagGroupEntity> builder) {
            builder.HasKey (p => p.Id);
            builder.HasMany<TagEntity> (x => x.Tags)
                .WithOne (y => y.TagGroupEntity);

            builder.Property (e => e.Name).HasMaxLength (255).IsUnicode ( ).IsRequired ( );
            builder.HasIndex (e => new { e.Name }).IsUnique ( );
        }
    }
}
