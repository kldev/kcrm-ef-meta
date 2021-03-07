using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KCrm.Core.Entity;
using KCrm.Core.Entity.Projects;
using KCrm.Core.Entity.Projects.Definitions;
using KCrm.Data.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KCrm.ConsoleSample {
    public class ProjectSample : SampleBase<ProjectContext> {
        private ProjectEntity CreateRandomProject(int i) {
            var projectId = Guid.NewGuid ( );
            var group = new ProjectEntity ( ) {
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

                await Context.Projects.AddAsync (new ProjectEntity  {
                    Id = ProjectHelper.ErpProjectOneId,
                    Name = "ERP Project 2020",
                    ProjectType = ProjectType.ERP,
                    Description = "Sample project with type ERP",
                    StartDateTimeUtc = DateTime.Parse ("2020/01/01 10:00"),
                    PlanedEndDateTimeUtc = null,
                    EndDateTimeUtc = null
                });

                await Context.ProjectActivities.AddAsync (new ProjectActivityEntity {
                    UserId = UserHelper.RootId,
                    ProjectId = ProjectHelper.ErpProjectOneId,
                    EventName = "created", EventData = new EventPayload (  ) {
                        EntityIDs = new List<Guid> (),
                        Value = "Project created"
                    }
                });

                await Context.Projects.AddAsync (new ProjectEntity {
                    Id = ProjectHelper.FinTechProjectOneId,
                    Name = "FinTech Project 2020",
                    ProjectType = ProjectType.FinTech,
                    Description = "Sample project with type FinTech"
                });

                await Context.Projects.AddAsync ( new ProjectEntity  {
                    Id = ProjectHelper.ErpProjectTwoId,
                    Name = "ERP Dev 2020",
                    ProjectType = ProjectType.ERP,
                    Description = "Sample project with type ERP to be deleted"
                });

                await Context.ProjectHasTags.AddAsync (new ProjectHasTagEntity ( ) { Id = Guid.NewGuid ( ), TagId = TagHelper.ImportantTagId, ProjectId = ProjectHelper.FinTechProjectOneId });
                await Context.ProjectHasTags.AddAsync (new ProjectHasTagEntity ( ) { Id = Guid.NewGuid ( ), TagId = TagHelper.EnglishTagId, ProjectId = ProjectHelper.FinTechProjectOneId });
                await Context.ProjectHasTags.AddAsync (new ProjectHasTagEntity ( ) { Id = Guid.NewGuid ( ), TagId = TagHelper.DesignOnly, ProjectId = ProjectHelper.ErpProjectOneId });
                await Context.ProjectHasTags.AddAsync (new ProjectHasTagEntity ( ) { Id = Guid.NewGuid ( ), TagId = TagHelper.GermanTagId, ProjectId = ProjectHelper.ErpProjectTwoId });

                await Context.ProjectHasUsers.AddAsync (new ProjectHasUserEntity ( ) {
                    Id = Guid.NewGuid ( ),
                    UserId = UserHelper.RootId,
                    UserRoleType = UserRoleInProjectType.Admin,
                    ProjectId = ProjectHelper.FinTechProjectOneId
                });

                await Context.ProjectHasUsers.AddAsync (new ProjectHasUserEntity ( ) {
                    Id = Guid.NewGuid ( ),
                    UserId = UserHelper.AdminId,
                    UserRoleType = UserRoleInProjectType.Admin,
                    ProjectId = ProjectHelper.ErpProjectOneId
                });

                await Context.ProjectHasUsers.AddAsync (new ProjectHasUserEntity ( ) {
                    Id = Guid.NewGuid ( ),
                    UserId = UserHelper.AliceId,
                    UserRoleType = UserRoleInProjectType.Member,
                    ProjectId = ProjectHelper.ErpProjectOneId
                });
                
              

                await Context.ProjectHasUsers.AddAsync (new ProjectHasUserEntity ( ) {
                    Id = Guid.NewGuid ( ),
                    UserId = UserHelper.JohnId,
                    UserRoleType = UserRoleInProjectType.Member,
                    ProjectId = ProjectHelper.ErpProjectOneId
                });

                await Context.ProjectHasUsers.AddAsync (new ProjectHasUserEntity ( ) {
                    Id = Guid.NewGuid ( ),
                    UserId = UserHelper.JohnId,
                    UserRoleType = UserRoleInProjectType.Member,
                    ProjectId = ProjectHelper.FinTechProjectOneId
                });
                
                await Context.ProjectActivities.AddAsync (new ProjectActivityEntity {
                    Id = 0,
                    UserId = UserHelper.RootId,
                    ProjectId = ProjectHelper.ErpProjectOneId,
                    EventName = "add_member", EventData = new EventPayload (  ) {
                        Value = "", EntityName = "Users", EntityIDs = new List<Guid> () {
                            UserHelper.AliceId, UserHelper.JohnId
                        }
                    }
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
