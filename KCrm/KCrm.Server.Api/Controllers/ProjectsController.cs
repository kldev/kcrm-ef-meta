using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KCrm.Core.Entity.Projects;
using KCrm.Logic.Core;
using KCrm.Logic.Services.Projects.Commands;
using KCrm.Logic.Services.Projects.Model;
using KCrm.Logic.Services.Projects.Queries;
using KCrm.Logic.Services.Tags.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace KCrm.Server.Api.Controllers {
    [ApiController]
    [Route ("api/projects")]
    public class ProjectsController : AppAuthorizedControllerBase {
        private readonly ILogger<ProjectsController> _logger;
        public ProjectsController(ILogger<ProjectsController> logger, IMediator mediator) : base (mediator) {
            _logger = logger;
        }

        [SwaggerResponse (200, "The auth result", typeof (List<ProjectEntity>))]
        [SwaggerResponse (500, "The error", typeof (ErrorDto))]
        [HttpGet]
        [Route ("")]
        public async Task<IActionResult> GetProjects() {
            var result = await _mediator.Send (new GetProjectsQuery ( ));
            return Ok (result);
        }

        [SwaggerResponse (200, "The auth result", typeof (List<TagDto>))]
        [SwaggerResponse (500, "The error", typeof (ErrorDto))]
        [HttpGet]
        [Route ("{projectId}/tags")]
        public async Task<IActionResult> Tags(Guid projectId) {
            var result = await _mediator.Send (new GetProjectTagsQuery { ProjectId = projectId });
            return Ok (result);
        }

        [SwaggerResponse (200, "The auth result", typeof (List<ProjectStartedStatDto>))]
        [SwaggerResponse (500, "The error", typeof (ErrorDto))]
        [HttpGet]
        [Route ("stats/{year?}")]
        public async Task<IActionResult> Stats(int year = 2020) {
            var stats = await _mediator.Send (new GetProjectStartedStatsQuery { Year = year });
            return Ok (stats);
        }


        [HttpPost]
        [Route ("create")]
        [SwaggerResponse (200, "Project created Id", typeof (EntityCreatedDto))]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand request) {
            var result = await _mediator.Send (request);

            return ApiCreatedResult (result);
        }
    }
}
