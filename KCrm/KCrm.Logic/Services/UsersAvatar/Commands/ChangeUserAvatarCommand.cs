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
    public class UploadUserAvatarCommand : AuthentiactedBaseRequest, IRequest<string> {
        public Guid TargetUserId { get; set; }
        public List<IFormFile> Files { get; set; }
        
        [JsonIgnore]
        public string FileId { get; set; }
    }

    public class UploadUserAvatarHandler : BaseHandler, IRequestHandler<UploadUserAvatarCommand, string> {
        private readonly AppUserContext _userContext;

        public UploadUserAvatarHandler(IMapper mapper, AppUserContext userContext) : base(mapper) {
            _userContext = userContext;
        }

        public async Task<string> Handle(UploadUserAvatarCommand request, CancellationToken cancellationToken) {

            var userExits = await _userContext.UserAccounts.AnyAsync (x => x.Id == request.TargetUserId, cancellationToken);

            if (userExits) {
                var user = await _userContext.UserAccounts.SingleAsync (x => x.Id == request.TargetUserId,
                    cancellationToken);
                user.AvatarId = request.FileId;
                _userContext.UserAccounts.Update (user);
                await _userContext.SaveChangesAsync (cancellationToken);
            }

            return "Avatar change";
        }
    }
}
