using KCrm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Core {
    public abstract class AppEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class {
        public void Configure(EntityTypeBuilder<T> builder) {
            ConfigureEntity (builder);

            builder.AddChange ( );
            builder.AddSoftDelete ( );
        }
        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}
