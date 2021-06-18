using System.Net;
using System.Threading.Tasks;
using KCrm.Logic.Core;
using KCrm.Logic.Security.Interfaces;
using KCrm.Logic.Services.Auth.Commands;
using KCrm.Logic.Services.Auth.Model;
using KCrm.Logic.Services.Users.Queries;
using KCrm.Server.Api.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KCrm.Server.Api.Controllers.Users {
    [Route ("api/users")]
    public class UsersController : AppAuthorizedControllerBase {

        private readonly IAuthenticateService _authenticateService;
        
        public UsersController(IAuthenticateService authenticateService, IMediator mediator) : base (mediator) {
            _authenticateService = authenticateService;
        }

        [HttpGet]
        [Route ("logout")]
        public async Task<IActionResult> LogOut(string refreshToken) {
            HttpContext.Response.Cookies.Delete (WebConstants.JwtCookieName);
            
            await _mediator.Send (new LogoutUserCommand {});

            return NoContent ( );
        }

        [HttpGet]
        [Route ("me")]
        public async Task<IActionResult> GetMe() {
            var result = await _mediator.Send (new GetUserInfoQuery ( ));

            return ApiResult (result);
        }

        [HttpGet]
        [Route ("session")]
        public IActionResult GetSession() {
            var user = AppUser;
            if (user != null) {
                var sessionResponse = new ResponseBase<SessionDto> (new SessionDto ( ) {
                    Username = user.Username, Fullname = user.FullName, Role = user.UserRole, AvatarId = user.AvatarId // create mapper
                });

                return ApiResult (sessionResponse);
            }

            return ApiError (new ErrorDto ( ) {Message = "Session ended"}, HttpStatusCode.Unauthorized);
        }
    }
}
