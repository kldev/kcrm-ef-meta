using KCrm.Logic.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KCrm.Server.Api.Controllers {

    [ApiController]
    [Authorize]
    public abstract class AppAuthorizedControllerBase : AppControllerBase {
        protected readonly IMediator _mediator;

        protected AppAuthorizedControllerBase(IMediator mediator) {
            _mediator = mediator;
        }

        protected AppUserIdentity AppUser => new AppUserIdentity (HttpContext.User);
    }
}
