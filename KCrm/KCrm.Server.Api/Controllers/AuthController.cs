using System.Net;
using System.Threading.Tasks;
using KCrm.Logic.Core;
using KCrm.Logic.Services.Auth.Commands;
using KCrm.Logic.Services.Auth.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KCrm.Server.Api.Controllers {
    [ApiController]
    [AllowAnonymous]
    [Route ("api/auth")]
    public class AuthController : AppControllerBase {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route ("login")]
        [SwaggerResponse (200, "The auth result", typeof (AuthDto))]
        [SwaggerResponse (401, "The auth failed", typeof (ErrorDto))]
        public async Task<IActionResult> Login(LoginUserCommand request) {

            var response = await _mediator.Send (request);

            return ApiResult (response, HttpStatusCode.Unauthorized);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route ("refresh/{refreshToken}")]
        [SwaggerResponse (200, "The refresh auth result", typeof (AuthDto))]
        [SwaggerResponse (401, "The auth failed", typeof (ErrorDto))]
        public async Task<IActionResult> RefreshToken(string refreshToken) {

            var response = await _mediator.Send (new RefreshTokenCommand ( ) {RefreshToken = refreshToken});

            return ApiResult (response, HttpStatusCode.Unauthorized);
        }
    }
}
