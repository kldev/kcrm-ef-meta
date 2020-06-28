using System.Threading;
using System.Threading.Tasks;
using KCrm.Logic.Security.Interfaces;
using KCrm.Logic.Services.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KCrm.Server.Api.Controllers {
    [Route ("api/users")]
    public class UsersController : AppAuthorizedControllerBase {

        private readonly IAuthenticateService _authenticateService;


        public UsersController(IAuthenticateService authenticateService, IMediator mediator) : base (mediator) {
            _authenticateService = authenticateService;
        }

        [HttpGet]
        [Route ("logout")]
        public async Task<IActionResult> LogOut() {

            await _authenticateService.LogOutAsync (AppUser.UserId, CancellationToken.None);

            return NoContent ( );
        }

        [HttpGet]
        [Route ("me")]
        public async Task<IActionResult> GetMe() {
            var result = await _mediator.Send (new GetUserInfoQuery ( ));

            return ApiResult (result);
        }
    }
}
