using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KCrm.Data.Users;
using KCrm.Logic.Core;
using KCrm.Logic.Services.Users.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Logic.Services.Users.Queries {
    public class GetUserInfoQuery : AuthentiactedBaseRequest, IRequest<ResponseBase<UserInfoDto>> {

    }

    public class GetUserInfoQueryHandler : BaseHandler, IRequestHandler<GetUserInfoQuery, ResponseBase<UserInfoDto>> {
        string UserNotFoundError => $"{nameof (GetUserInfoQuery)}.UserNotFound";

        private readonly AppUserContext _appUserContext;

        public GetUserInfoQueryHandler(IMapper mapper, AppUserContext appUserContext) : base (mapper) {
            _appUserContext = appUserContext;
        }

        public async Task<ResponseBase<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken) {

            var user = await _appUserContext.UserAccounts.FirstAsync (x => x.Id == request.UserId, cancellationToken);

            if (user == null) {
                return new ResponseBase<UserInfoDto> (new ErrorDto ( ) {
                    ErrorCode = UserNotFoundError,
                    Message = "User not found"
                });
            }

            return new ResponseBase<UserInfoDto> (_mapper.Map<UserInfoDto> (user));
        }
    }
}
