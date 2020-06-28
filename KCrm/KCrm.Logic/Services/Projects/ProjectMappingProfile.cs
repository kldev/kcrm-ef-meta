using System;
using AutoMapper;
using KCrm.Core.Entity.Projects;
using KCrm.Data.Aegis.Entity;
using KCrm.Logic.Services.Projects.Commands;
using KCrm.Logic.Services.Projects.Model;

namespace KCrm.Logic.Services.Projects {
    public class ProjectMappingProfile : Profile {
        public ProjectMappingProfile() {
            ProjectMapping ( );
        }

        private void ProjectMapping() {
            CreateMap<Project, ProjectDto> ( ).ReverseMap ( );
            CreateMap<ProjectStartedStats, ProjectStartedStatDto> ( )
                .ForMember (x => x.Month, y => y.MapFrom<int> (src => (int)src.Monthnumber))
                .ForMember (x => x.Year, y => y.MapFrom<int> (src => int.Parse (src.Year)))
                .ForMember (x => x.Count, y => y.MapFrom<int> (src => (int)(src.Count)))
                .ReverseMap ( );

            CreateMap<CreateProjectCommand, Project> ( ).ConstructUsing (x => new Project ( ) {
                Id = Guid.NewGuid ( ),
                Description = x.Description,
                Name = x.Name,
                ProjectType = x.ProjectType,
                EndDateTimeUtc = null,
                PlanedEndDateTimeUtc = x.PlanedEndDateTimeUtc,
                StartDateTimeUtc = x.StartDateTimeUtc
            });
        }
    }
}
