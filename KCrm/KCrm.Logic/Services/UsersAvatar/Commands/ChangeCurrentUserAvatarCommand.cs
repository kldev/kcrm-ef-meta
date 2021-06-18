using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KCrm.Data.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KCrm.Logic.Services.UsersAvatar.Commands  {
    public class ChangeCurrentUserAvatarCommand : AuthentiactedBaseRequest, IRequest<string> {
        
        public List<IFormFile> Files { get; set; }
        
        [JsonIgnore]
        public string FileId { get; set; }
    }

    public class ChangeCurrentUserAvatarCommandHandler : BaseHandler, IRequestHandler<ChangeCurrentUserAvatarCommand, string> {
        private readonly AppUserContext _userContext;

        public ChangeCurrentUserAvatarCommandHandler(IMapper mapper, AppUserContext userContext) : base(mapper) {
            _userContext = userContext;
        }

        public async Task<string> Handle(ChangeCurrentUserAvatarCommand request, CancellationToken cancellationToken) {

            var userExits = await _userContext.UserAccounts.AnyAsync (x => x.Id == request.UserId, cancellationToken);

            if (userExits) {
                var user = await _userContext.UserAccounts.SingleAsync (x => x.Id == request.UserId,
                    cancellationToken);
                user.AvatarId = request.FileId;
                _userContext.UserAccounts.Update (user);
                await _userContext.SaveChangesAsync (cancellationToken);
            }

            return "Avatar change";
        }
    }
}
