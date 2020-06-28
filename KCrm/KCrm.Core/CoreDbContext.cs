using System;
using System.Threading;
using System.Threading.Tasks;
using KCrm.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Core {
    public abstract class CoreDbContext : DbContext {

        protected CoreDbContext() {

        }
        protected CoreDbContext(DbContextOptions options) : base (options) {

        }

        public override int SaveChanges() {
            ChangeTracker.DetectChanges ( );

            foreach (var entry in ChangeTracker.Entries ( )) {
                if (entry.Entity is IChange) {
                    if (entry.State == EntityState.Added) {
                        entry.Property ("Created").CurrentValue = DateTime.UtcNow;
                    }

                    if (entry.State == EntityState.Modified) {
                        entry.Property ("Updated").CurrentValue = DateTime.UtcNow;
                    }
                }

                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDelete) {
                    entry.State = EntityState.Modified;
                    entry.Property ("Deleted").CurrentValue = true;
                }
            }

            return base.SaveChanges ( );
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken ( )) {
            ChangeTracker.DetectChanges ( );

            foreach (var entry in ChangeTracker.Entries ( )) {
                if (entry.Entity is IChange) {
                    if (entry.State == EntityState.Added) {
                        entry.Property ("Created").CurrentValue = DateTime.UtcNow;
                    }

                    if (entry.State == EntityState.Modified) {
                        entry.Property ("Updated").CurrentValue = DateTime.UtcNow;
                    }
                }

                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDelete) {
                    entry.State = EntityState.Modified;
                    entry.Property ("Deleted").CurrentValue = true;
                }
            }

            return base.SaveChangesAsync (cancellationToken);
        }
    }
}
