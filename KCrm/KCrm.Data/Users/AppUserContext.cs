using System;
using KCrm.Core;
using KCrm.Core.Entity.Users;
using KCrm.Data.Users.Config;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Data.Users {
    public class AppUserContext : CoreDbContext {

        public AppUserContext() {

        }
        public AppUserContext(DbContextOptions<AppUserContext> options) : base (options) {
        }

        public DbSet<UserAccountEntity> UserAccounts { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<UserHasRoleEntity> UserHasRoles { get; set; }
        public DbSet<UserLoginActivityEntity> UserLogins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.HasDefaultSchema ("app");
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (UserConfig).Assembly,  x => x.Namespace != null && x.Namespace.IndexOf ("Data.Users", StringComparison.Ordinal) > -1);

            base.OnModelCreating (modelBuilder);
        }
    }
}
