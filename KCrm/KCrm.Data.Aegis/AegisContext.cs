using KCrm.Data.Aegis.Entity;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Data.Aegis {
    public partial class AegisContext : DbContext {
        public AegisContext() {
        }

        public AegisContext(DbContextOptions<AegisContext> options)
            : base (options) {
        }

        public virtual DbSet<ProjectStartedStats> ProjectStartedStats { get; set; }
        public virtual DbSet<ProjectTags> ProjectTags { get; set; }
        public virtual DbSet<Schemaversions> Schemaversions { get; set; }
        public virtual DbSet<UserAndRoles> UserAndRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql ("Name=AegisConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ProjectStartedStats> (entity => {
                entity.HasNoKey ( );

                entity.ToTable ("project_started_stats", "aegis");

                entity.Property (e => e.Count).HasColumnName ("count");

                entity.Property (e => e.Month).HasColumnName ("month");

                entity.Property (e => e.Monthnumber).HasColumnName ("monthnumber");

                entity.Property (e => e.Year).HasColumnName ("year");
            });

            modelBuilder.Entity<ProjectTags> (entity => {
                entity.HasNoKey ( );

                entity.ToTable ("project_tags", "aegis");

                entity.Property (e => e.Projectname)
                    .HasColumnName ("projectname")
                    .HasMaxLength (1024);

                entity.Property (e => e.Projectstags)
                    .HasColumnName ("projectstags")
                    .HasColumnType ("json");
            });

            modelBuilder.Entity<Schemaversions> (entity => {
                entity.ToTable ("schemaversions", "aegis");

                entity.Property (e => e.Schemaversionsid).HasColumnName ("schemaversionsid");

                entity.Property (e => e.Applied).HasColumnName ("applied");

                entity.Property (e => e.Scriptname)
                    .IsRequired ( )
                    .HasColumnName ("scriptname")
                    .HasMaxLength (255);
            });

            modelBuilder.Entity<UserAndRoles> (entity => {
                entity.HasNoKey ( );

                entity.ToTable ("user_and_roles", "aegis");

                entity.Property (e => e.Id).HasColumnName ("id");

                entity.Property (e => e.Roles)
                    .HasColumnName ("roles")
                    .HasColumnType ("json");

                entity.Property (e => e.Topusers)
                    .HasColumnName ("topusers")
                    .HasColumnType ("json");

                entity.Property (e => e.UserId).HasColumnName ("user_id");

                entity.Property (e => e.UserRoleId).HasColumnName ("user_role_id");
            });

            OnModelCreatingPartial (modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
