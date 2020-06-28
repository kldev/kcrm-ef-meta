using System.Threading;
using System.Threading.Tasks;
using KCrm.Logic.Core;
using KCrm.Logic.Security.Interfaces;
using KCrm.Logic.Services.Auth.Model;
using MediatR;

namespace KCrm.Logic.Services.Auth.Commands {
    public class LoginUserCommand : IRequest<ResponseBase<AuthDto>> {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResponseBase<AuthDto>> {

        private readonly IAuthenticateService _authenticateService;
        
        public LoginUserCommandHandler(IAuthenticateService authenticateService) {
            _authenticateService = authenticateService;
        }

        public async  Task<ResponseBase<AuthDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken) {

            var result = await _authenticateService.LoginAsync (request.Username, request.Password, cancellationToken);

            return result;
        }
    }
  
}
