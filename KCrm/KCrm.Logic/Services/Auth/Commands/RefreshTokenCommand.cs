using System.Threading;
using System.Threading.Tasks;
using KCrm.Logic.Core;
using KCrm.Logic.Security.Interfaces;
using KCrm.Logic.Services.Auth.Model;
using MediatR;

namespace KCrm.Logic.Services.Auth.Commands {
    public class RefreshTokenCommand : IRequest<ResponseBase<AuthDto>> {
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ResponseBase<AuthDto>> {
        
        private readonly IAuthenticateService _authenticateService;

        public RefreshTokenCommandHandler(IAuthenticateService authenticateService) {
            _authenticateService = authenticateService;
        }

        public async Task<ResponseBase<AuthDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken) {
            var result = await _authenticateService.UseRefreshTokenAsync (request.RefreshToken, cancellationToken);
            return result;
        }
    }
}
