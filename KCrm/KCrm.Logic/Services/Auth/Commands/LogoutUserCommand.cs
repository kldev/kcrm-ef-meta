using System.Threading;
using System.Threading.Tasks;
using KCrm.Logic.Security.Interfaces;
using MediatR;

namespace KCrm.Logic.Services.Auth.Commands {
    public class LogoutUserCommand : AuthentiactedBaseRequest, IRequest {
        public string RefreshToken { get; set; }
    }

    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand> {
        private readonly IAuthenticateService _authenticateService;

        public LogoutUserCommandHandler(IAuthenticateService authenticateService) {
            _authenticateService = authenticateService;
        }

        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken) {
            await _authenticateService.LogOutAsync (request.UserId, cancellationToken);
            return default;
        }
    }
    
    
    
}
