using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KCrm.Core.Entity.Tags;
using KCrm.Data.Context;
using KCrm.Logic.Services.Tags.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KCrm.Logic.Services.Projects.Queries {
    public class GetProjectTagsQuery : AuthentiactedBaseRequest, IRequest<IEnumerable<TagDto>> {
        public Guid ProjectId { get; set; }
    }

    public class GetProjectTagsQueryHandler : BaseHandler, IRequestHandler<GetProjectTagsQuery, IEnumerable<TagDto>> {

        private readonly ProjectContext _projectContext;
        private readonly TagContext _tagContext;
        private readonly ILogger<GetProjectTagsQueryHandler> _logger;

        public GetProjectTagsQueryHandler(IMapper mapper, ILogger<GetProjectTagsQueryHandler> logger, ProjectContext projectContext, TagContext tagContext) : base (mapper) {
            _logger = logger;
            _projectContext = projectContext;
            _tagContext = tagContext;
        }

        public async Task<IEnumerable<TagDto>>
            Handle(GetProjectTagsQuery request, CancellationToken cancellationToken) {
            _logger.LogInformation ($"User {request.UserId} -> GetProjectTagsQuery");
            var ids = await GetProjectTagIdsAsync (request.ProjectId, cancellationToken);
            if (ids.Count > 0) {
                var result = await GetBatchAsync (ids.ToArray ( ), cancellationToken);
                return _mapper.Map<List<TagDto>> (result);
            }

            return new List<TagDto> ( );
        }
        
        private async Task<List<Tag>> GetBatchAsync(Guid[] ids, CancellationToken cancellationToken) {

            if (ids == null || ids.Length == 0) throw new ArgumentException ("Tag Ids must be provided");

            var idsQuery = from y in ids select y;

            var query = _tagContext.Tags.AsNoTracking ( ).Where (tag => idsQuery.Contains (tag.Id));

            return await query.ToListAsync ( cancellationToken);
        }

        
        private async Task<List<Guid>> GetProjectTagIdsAsync(Guid projectId, CancellationToken cancellationToken) {
          //  var idsQuery = (from id in projectIds select id);
          //  await _projectContext.ProjectHasTags.AsNoTracking ( ).Where (x => idsQuery.Contains (x.ProjectId))

            return await _projectContext.ProjectHasTags.AsNoTracking ( ).Where (x => x.ProjectId == projectId)
                .Select (x => x.TagId).ToListAsync ( cancellationToken);
        }

    }
}
