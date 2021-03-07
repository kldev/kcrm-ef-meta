using System;
using KCrm.Core;
using KCrm.Core.Entity.GeoLocation;
using KCrm.Data.GeoLocation.Config;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Data.GeoLocation {
    public class GeoLocationContext : CoreDbContext {
        public GeoLocationContext() {

        }
        public GeoLocationContext(DbContextOptions<GeoLocationContext> options) : base (options) {
        }

        public DbSet<CountryEntity> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.HasDefaultSchema ("common");
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (CountryConfig).Assembly, x => x.Namespace != null && x.Namespace.IndexOf ("Data.GeoLocation", StringComparison.Ordinal) > -1);

            base.OnModelCreating (modelBuilder);
        }
    }
}
