using System;
using KCrm.Core;
using KCrm.Core.Entity.Tags;
using KCrm.Data.Tags.Config;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Data.Tags {
    public class TagContext : CoreDbContext {

        public TagContext() {

        }
        public TagContext(DbContextOptions<TagContext> options) : base (options) {
        }

        public virtual DbSet<TagEntity> Tags { get; set; }
        public virtual DbSet<TagGroupEntity> TagGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema ("tag");
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (TagConfig).Assembly,  x => x.Namespace != null && x.Namespace.IndexOf ("Data.Tags", StringComparison.Ordinal) > -1);

            base.OnModelCreating (modelBuilder);
        }
    }
}
