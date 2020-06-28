using KCrm.Core;
using KCrm.Core.Entity.Projects;
using KCrm.Data.Config.Projects;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Data.Context {
    public class ProjectContext : CoreDbContext {

        public ProjectContext() {

        }
        public ProjectContext(DbContextOptions<ProjectContext> options) : base (options) { }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<ProjectHasTag> ProjectHasTags { get; set; }

        public virtual DbSet<ProjectHasUser> ProjectHasUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.HasDefaultSchema ("project");
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (ProjectConfig).Assembly, x => x.Namespace.IndexOf ("Config.Projects") > -1);

            base.OnModelCreating (modelBuilder);
        }
    }
}
