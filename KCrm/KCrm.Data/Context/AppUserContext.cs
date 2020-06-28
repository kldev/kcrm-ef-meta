using KCrm.Core;
using KCrm.Core.Entity.Users;
using KCrm.Data.Config.Users;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Data.Context {
    public class AppUserContext : CoreDbContext {

        public AppUserContext() {

        }
        public AppUserContext(DbContextOptions<AppUserContext> options) : base (options) {
        }

        public DbSet<User> AppUsers { get; set; }
        public DbSet<UserRole> AppUserRoles { get; set; }
        public DbSet<UserHasRole> UserHasRoles { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.HasDefaultSchema ("app");
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (UserConfig).Assembly, x => x.Namespace.IndexOf ("Config.Users") > -1);

            base.OnModelCreating (modelBuilder);
        }
    }
}
