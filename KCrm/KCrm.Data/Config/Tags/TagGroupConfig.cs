using KCrm.Core;
using KCrm.Core.Entity.Tags;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Config.Tags {
    public class TagGroupConfig : AppEntityTypeConfiguration<TagGroup> {
        protected override void ConfigureEntity(EntityTypeBuilder<TagGroup> builder) {
            builder.HasKey (p => p.Id);
            builder.HasMany<Tag> (x => x.Tags)
                .WithOne (y => y.TagGroup);

            builder.Property (e => e.Name).HasMaxLength (255).IsUnicode ( ).IsRequired ( );
            builder.HasIndex (e => new { e.Name }).IsUnique ( );
        }
    }
}
