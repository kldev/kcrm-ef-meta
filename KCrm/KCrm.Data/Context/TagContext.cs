using KCrm.Core;
using KCrm.Core.Entity.Tags;
using KCrm.Data.Config.Tags;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Data.Context {
    public class TagContext : CoreDbContext {

        public TagContext() {

        }
        public TagContext(DbContextOptions<TagContext> options) : base (options) {
        }

        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagGroup> TagGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema ("tag");
            modelBuilder.ApplyConfiguration (new TagConfig ( ));
            modelBuilder.ApplyConfiguration (new TagGroupConfig ( ));

            base.OnModelCreating (modelBuilder);
        }
    }
}
