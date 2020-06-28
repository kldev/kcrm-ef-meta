using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using KCrm.Core.Entity.Projects;
using KCrm.Core.Exceptions;
using KCrm.Data.Context;
using KCrm.Logic.Services.Projects.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Logic.Services.Projects.Commands {
    public class CreateProjectCommand : AuthentiactedBaseRequest, IRequest<Guid> {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public ProjectType ProjectType { get; set; } = ProjectType.FinTech;
        public DateTime? StartDateTimeUtc { get; set; }
        public DateTime? PlanedEndDateTimeUtc { get; set; }
    }

    public class CreateProjectCommandHandler : BaseHandler, IRequestHandler<CreateProjectCommand, Guid> {
        private readonly ProjectContext _projectContext;
        private IMediator _mediator;
        public CreateProjectCommandHandler(IMapper mapper, ProjectContext projectContext, IMediator mediator) : base (mapper) {
            _projectContext = projectContext;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken) {

            var project = _mapper.Map<Project> (request);

            var alreadyUsedName = await _projectContext.Projects.AsNoTracking ( )
                .AnyAsync (x => x.Name == request.Name, cancellationToken);

            if (alreadyUsedName)
                throw new AppLogicRuleException ($"{nameof (CreateProjectCommand)}.NameAlreadyUsed",
                    "The name is already used");

            await _projectContext.Projects.AddAsync (project, cancellationToken);
            await _projectContext.SaveChangesAsync (cancellationToken);

            await _mediator.Publish (new SendMailOnProjectCreated ( ) { ProjectId = project.Id });

            return project.Id;
        }
    }

    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand> {
        public CreateProjectCommandValidator() {
            RuleFor (x => x.Name).NotNull ( ).WithErrorCode ($"{nameof (CreateProjectCommand)}.Name.NotNull").WithMessage ("Can not be null or empty");
            RuleFor (x => x.Name).MaximumLength (120)
                .WithErrorCode ($"{nameof (CreateProjectCommand)}.Name.MaxLength120");
            RuleFor (x => x.Name).MinimumLength (3)
                .WithErrorCode ($"{nameof (CreateProjectCommand)}.Name.MinimumLength3")
                .WithMessage ("Minimum length is 3 characters");
        }
    }

}
