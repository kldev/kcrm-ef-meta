using System;
using KCrm.Core;
using KCrm.Core.Entity.Projects;
using KCrm.Data.Projects.Config;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Data.Projects {
    public class ProjectContext : CoreDbContext {

        public ProjectContext() {
        }
        public ProjectContext(DbContextOptions<ProjectContext> options) : base (options) { }
        
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<ProjectHasTagEntity> ProjectHasTags { get; set; }
        public DbSet<ProjectHasUserEntity> ProjectHasUsers { get; set; }
        public DbSet<ProjectActivityEntity> ProjectActivities { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.HasDefaultSchema ("project");
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (ProjectConfig).Assembly,  x => x.Namespace != null && x.Namespace.IndexOf ("Data.Projects.Config", StringComparison.Ordinal) > -1);

            base.OnModelCreating (modelBuilder);
        }
    }
}
