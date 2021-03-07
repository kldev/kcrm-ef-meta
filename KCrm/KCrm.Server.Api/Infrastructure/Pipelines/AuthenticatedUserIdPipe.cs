using System.Threading;
using System.Threading.Tasks;
using KCrm.Logic.Security;
using KCrm.Logic.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace KCrm.Server.Api.Infrastructure.Pipelines {
    public class AuthenticatedUserIdPipe<TIn, TOut> : IPipelineBehavior<TIn, TOut> {

        private readonly HttpContext _httpContext;

        public AuthenticatedUserIdPipe(IHttpContextAccessor accessor) {
            _httpContext = accessor.HttpContext;
        }

        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next) {

            if (request is AuthentiactedBaseRequest baseRequest) {
                var appUser = new AppUserIdentity (_httpContext.User);
                baseRequest.SetFromContext (appUser.UserId, appUser.UserRole);

            }

            return await next ( );
        }
    }
}
