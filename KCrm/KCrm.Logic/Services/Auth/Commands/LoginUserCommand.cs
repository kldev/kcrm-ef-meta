using System.Threading;
using System.Threading.Tasks;
using KCrm.Logic.Core;
using KCrm.Logic.Security.Interfaces;
using KCrm.Logic.Services.Auth.Model;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace KCrm.Logic.Services.Auth.Commands {
    public class LoginUserCommand : IRequest<ResponseBase<AuthDto>> {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResponseBase<AuthDto>> {

        private readonly IAuthenticateService _authenticateService;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginUserCommandHandler(IAuthenticateService authenticateService, IHttpContextAccessor contextAccessor) {
            _authenticateService = authenticateService;
            _contextAccessor = contextAccessor;
        }

        public async  Task<ResponseBase<AuthDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken) {

            var result = await _authenticateService.LoginAsync (request.Username, request.Password, cancellationToken);

            return result;
        }
    }
  
}
