using System;
using System.Net;
using System.Threading.Tasks;
using KCrm.Logic.Core;
using KCrm.Logic.Services.Auth.Commands;
using KCrm.Logic.Services.Auth.Model;
using KCrm.Server.Api.Config;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [SwaggerResponse (200, "The auth result", typeof(AuthDto))]
        [SwaggerResponse (401, "The auth failed", typeof(ErrorDto))]
        public async Task<IActionResult> Login(LoginUserCommand request) {

            var response = await _mediator.Send (request);

            var result = response.Data;

            if (response.Data != null) {
                HttpContext.Response.Cookies.Append (WebConstants.JwtCookieName, result.Token,
                    new CookieOptions ( ) {
                        HttpOnly = true,
                        Path = "/",
                        Secure = false, // for production with HTTP should be true
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.Now.AddDays (1),
                    });

                var sessionResponse = new ResponseBase<SessionDto> (new SessionDto ( ) {
                    Username = result.Username, Fullname = result.FullName, Role = result.Role
                });

                return ApiResult (sessionResponse);
            }

            return ApiError (response.Error, HttpStatusCode.Unauthorized);
        }
    }
}
