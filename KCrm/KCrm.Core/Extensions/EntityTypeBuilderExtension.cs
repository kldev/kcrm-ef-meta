using System;
using KCrm.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Core.Extensions {
    public static class EntityTypeBuilderExtension {
        public static EntityTypeBuilder<T> AddSoftDelete<T>(this EntityTypeBuilder<T> builder) where T : class {
            if (typeof (ISoftDelete).IsAssignableFrom (typeof (T))) {
                builder.Property<bool> ("Deleted");
                builder.HasQueryFilter (p => EF.Property<bool> (p, "Deleted") == false);
            }

            return builder;
        }

        public static EntityTypeBuilder<T> AddChange<T>(this EntityTypeBuilder<T> builder) where T : class {
            if (typeof (IChange).IsAssignableFrom (typeof (T))) {
                builder.Property<DateTime?> ("Created")
                    .HasConversion (x => x,
                        x => x.HasValue ? DateTime.SpecifyKind (x.Value, DateTimeKind.Utc) : default (DateTime?)); ;
                builder.Property<DateTime?> ("Updated")
                    .HasConversion (x => x,
                        x => x.HasValue ? DateTime.SpecifyKind (x.Value, DateTimeKind.Utc) : default (DateTime?)); ;
            }

            return builder;
        }
    }
}
