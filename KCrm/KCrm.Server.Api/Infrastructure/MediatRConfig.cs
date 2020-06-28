using FluentValidation;
using KCrm.Logic.Behaviors;
using KCrm.Logic.Services.Projects.Commands;
using KCrm.Logic.Services.Projects.Queries;
using KCrm.Server.Api.Infrastructure.Pipelines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KCrm.Server.Api.Infrastructure {
    public static class MediatRConfig {
        public static IServiceCollection AddAppMediatR(this IServiceCollection services) {

            services.AddMediatR (typeof (GetProjectsQuery).Assembly);
            services.AddScoped (typeof (IPipelineBehavior<,>), typeof (AuthenticatedUserIdPipe<,>));
            services.AddTransient (typeof (IPipelineBehavior<,>), typeof (ValidationBehavior<,>));
            services.AddValidatorsFromAssembly (typeof (CreateProjectCommandValidator).Assembly);

            return services;
        }
    }
}
