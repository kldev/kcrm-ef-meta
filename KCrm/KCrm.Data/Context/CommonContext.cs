using KCrm.Core;
using KCrm.Core.Entity.Common;
using KCrm.Data.Config.Common;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Data.Context {
    public class CommonContext : CoreDbContext {
        public CommonContext() {

        }
        public CommonContext(DbContextOptions<CommonContext> options) : base (options) {
        }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.HasDefaultSchema ("common");
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (CountryConfig).Assembly, x => x.Namespace.IndexOf ("Config.Common") > -1);

            base.OnModelCreating (modelBuilder);
        }
    }
}
