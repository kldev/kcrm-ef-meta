using System;
using System.Linq;
using System.Threading.Tasks;
using KCrm.Core.Entity.Projects;
using KCrm.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KCrm.ConsoleSample {
    public class ProjectSample : SampleBase<ProjectContext> {
        private Project CreateRandomProject(int i) {
            var projectId = Guid.NewGuid ( );
            var group = new Project ( ) {
                Name = $"Random Project {i}",
                Id = projectId,
                ProjectType = (ProjectType)(100 + _random.Next (0, 5)),
                StartDateTimeUtc = DateTime.Now.AddDays (_random.Next (100, 400)),
                Description = string.Empty
            };
            return group;
        }

        protected override async Task SeedData() {
            if (await Context.Projects.CountAsync ( ) == 0) {

                _logger.LogInformation ("Seed projects");

                Context.Projects.Add (new Project ( ) {
                    Id = ProjectHelper.ErpProjectOneId,
                    Name = "ERP Project 2020",
                    ProjectType = ProjectType.ERP,
                    Description = "Sample project with type ERP",
                    StartDateTimeUtc = DateTime.Parse ("2020/01/01 10:00"),
                    PlanedEndDateTimeUtc = null,
                    EndDateTimeUtc = null
                });

                Context.Projects.Add (new Project ( ) {
                    Id = ProjectHelper.FinTechProjectOneId,
                    Name = "FinTech Project 2020",
                    ProjectType = ProjectType.FinTech,
                    Description = "Sample project with type FinTech"
                });

                Context.Projects.Add (new Project ( ) {
                    Id = ProjectHelper.ErpProjectTwoId,
                    Name = "ERP Dev 2020",
                    ProjectType = ProjectType.ERP,
                    Description = "Sample project with type ERP to be deleted"
                });

                Context.ProjectHasTags.Add (new ProjectHasTag ( ) { Id = Guid.NewGuid ( ), TagId = TagHelper.ImportantTagId, ProjectId = ProjectHelper.FinTechProjectOneId });
                Context.ProjectHasTags.Add (new ProjectHasTag ( ) { Id = Guid.NewGuid ( ), TagId = TagHelper.EnglishTagId, ProjectId = ProjectHelper.FinTechProjectOneId });
                Context.ProjectHasTags.Add (new ProjectHasTag ( ) { Id = Guid.NewGuid ( ), TagId = TagHelper.DesignOnly, ProjectId = ProjectHelper.ErpProjectOneId });
                Context.ProjectHasTags.Add (new ProjectHasTag ( ) { Id = Guid.NewGuid ( ), TagId = TagHelper.GermanTagId, ProjectId = ProjectHelper.ErpProjectTwoId });

                Context.ProjectHasUsers.Add (new ProjectHasUser ( ) {
                    Id = Guid.NewGuid ( ),
                    UserId = UserHelper.RootId,
                    UserRoleType = UserRoleInProjectType.Admin,
                    ProjectId = ProjectHelper.FinTechProjectOneId
                });

                Context.ProjectHasUsers.Add (new ProjectHasUser ( ) {
                    Id = Guid.NewGuid ( ),
                    UserId = UserHelper.AdminId,
                    UserRoleType = UserRoleInProjectType.Admin,
                    ProjectId = ProjectHelper.ErpProjectOneId
                });

                Context.ProjectHasUsers.Add (new ProjectHasUser ( ) {
                    Id = Guid.NewGuid ( ),
                    UserId = UserHelper.AliceId,
                    UserRoleType = UserRoleInProjectType.Member,
                    ProjectId = ProjectHelper.ErpProjectOneId
                });

                Context.ProjectHasUsers.Add (new ProjectHasUser ( ) {
                    Id = Guid.NewGuid ( ),
                    UserId = UserHelper.JohnId,
                    UserRoleType = UserRoleInProjectType.Member,
                    ProjectId = ProjectHelper.ErpProjectOneId
                });

                Context.ProjectHasUsers.Add (new ProjectHasUser ( ) {
                    Id = Guid.NewGuid ( ),
                    UserId = UserHelper.JohnId,
                    UserRoleType = UserRoleInProjectType.Member,
                    ProjectId = ProjectHelper.FinTechProjectOneId
                });

                /*var i = 0;
                var range = new List<Project> ( );
                while (i++ < 1000) {
                    range.Add (CreateRandomProject (i));
                }

                await Context.Projects.AddRangeAsync (range);
*/
                await Context.SaveChangesAsync ( );
                var projectFinTech = await Context.Projects.Where (x => x.Id.Equals (ProjectHelper.FinTechProjectOneId))
                    .FirstOrDefaultAsync ( );
                projectFinTech.Description = "Changed description";

                Context.Update (projectFinTech);

                var projectErp2 = await Context.Projects.Where (x => x.Id.Equals (ProjectHelper.ErpProjectTwoId))
                    .FirstOrDefaultAsync ( );

                Context.Remove (projectErp2);

                await Context.SaveChangesAsync ( );

                _logger.LogInformation ("Seed completed");
            }
            else {
                _logger.LogInformation ("projects already seeded");
            }
        }
    }
}
