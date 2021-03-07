using KCrm.Core;
using KCrm.Core.Entity.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Tags.Config {
    public class TagConfig : AppEntityTypeConfiguration<TagEntity> {
        protected override void ConfigureEntity(EntityTypeBuilder<TagEntity> builder) {
            builder.HasKey (p => p.Id);

            builder.HasOne<TagGroupEntity> (p => p.TagGroupEntity)
                .WithMany (e => e.Tags).HasForeignKey (e => e.TagGroupId)
                .OnDelete (DeleteBehavior.SetNull);

            builder.Property (e => e.Name).HasMaxLength (255).IsUnicode ( ).IsRequired ( );
            builder.HasIndex (x => new { x.Name, x.TagGroupId }).IsUnique ( );
            builder.HasIndex (e => e.TagGroupId);
        }
    }
}
