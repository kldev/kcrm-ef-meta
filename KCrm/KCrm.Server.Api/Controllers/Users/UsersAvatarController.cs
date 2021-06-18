using System;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using KCrm.Core.Definition;
using KCrm.Logic.Core;
using KCrm.Logic.Services.UsersAvatar.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KCrm.Server.Api.Controllers.Users {
    
    [Route("api/users-avatar")]
    public class UsersAvatarController : AppAuthorizedControllerBase {
        private readonly IAmazonS3 _amazonS3;

        public UsersAvatarController(IMediator mediator, IAmazonS3 amazonS3) : base(mediator) {
            _amazonS3 = amazonS3;
        }

        [AllowAnonymous]
        [HttpGet("{avatarId}")]
        public async  Task<IActionResult> Get(string avatarId) {

            if (string.IsNullOrEmpty (avatarId)) {
                return NotFound ( );
            }

            var objectStream = await _amazonS3.GetObjectStreamAsync (FilesS3BucketNames.Avatars, avatarId, null);
            var metadata = await _amazonS3.GetObjectMetadataAsync (FilesS3BucketNames.Avatars, avatarId);

            var contentType = metadata.Metadata.Keys.Contains ("x-app-content-type")
                ? metadata.Metadata["content-type"]
                : "image/png";

            return new FileStreamResult (objectStream, contentType);
        }
        
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm]UploadUserAvatarCommand model) {

            if (model?.Files == null) return BadRequest ("Not files uploaded");

            var guid = Guid.NewGuid ( ).ToString ("N");
            
            await using var fileStream = model.Files[0].OpenReadStream ( );
            var request = new PutObjectRequest ( ) {
                Key = guid, BucketName = FilesS3BucketNames.Avatars, ContentType = "image/png", InputStream = fileStream
            };
            request.Metadata.Add ("x-app-content-type", "image/png");
            
            await _amazonS3.PutObjectAsync (request);

            model.FileId = guid;

            await _mediator.Send (model);
            
            return Ok (new {avatarId = guid});
        }
        
        [HttpPost("upload-self")]
        public async Task<IActionResult> Upload([FromForm]ChangeCurrentUserAvatarCommand model) {

            if (model?.Files == null) return BadRequest ("Not files uploaded");

            var guid = Guid.NewGuid ( ).ToString ("N");

            await using var fileStream = model.Files[0].OpenReadStream ( );
            var request = new PutObjectRequest ( ) {
                Key = guid, BucketName = FilesS3BucketNames.Avatars, ContentType = "image/png", InputStream =  fileStream
            };
            request.Metadata.Add ("x-app-content-type", "image/png");
            
            await _amazonS3.PutObjectAsync (request);

            model.FileId = guid;

            await _mediator.Send (model);
            
            return Ok (new {avatarId = guid});
        }
        
    }
}
