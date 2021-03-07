using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using KCrm.Data.Aegis.Entity;

#nullable disable

namespace KCrm.Data.Aegis
{
    public partial class AegisContext : DbContext
    {
        public AegisContext()
        {
        }

        public AegisContext(DbContextOptions<AegisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProjectStartedStat> ProjectStartedStats { get; set; }
        public virtual DbSet<ProjectTag> ProjectTags { get; set; }
        public virtual DbSet<Schemaversion> Schemaversions { get; set; }
        public virtual DbSet<UserAndRole> UserAndRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=AegisConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<ProjectStartedStat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("project_started_stats", "aegis");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Monthnumber).HasColumnName("monthnumber");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<ProjectTag>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("project_tags", "aegis");

                entity.Property(e => e.Projectname)
                    .HasMaxLength(1024)
                    .HasColumnName("projectname");

                entity.Property(e => e.Projectstags)
                    .HasColumnType("json")
                    .HasColumnName("projectstags");
            });

            modelBuilder.Entity<Schemaversion>(entity =>
            {
                entity.HasKey(e => e.Schemaversionsid)
                    .HasName("PK_schemaversions_Id");

                entity.ToTable("schemaversions", "aegis");

                entity.Property(e => e.Schemaversionsid).HasColumnName("schemaversionsid");

                entity.Property(e => e.Applied).HasColumnName("applied");

                entity.Property(e => e.Scriptname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("scriptname");
            });

            modelBuilder.Entity<UserAndRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_and_roles", "aegis");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Roles)
                    .HasColumnType("json")
                    .HasColumnName("roles");

                entity.Property(e => e.Topusers)
                    .HasColumnType("json")
                    .HasColumnName("topusers");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
