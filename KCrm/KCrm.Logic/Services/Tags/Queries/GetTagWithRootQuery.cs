using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KCrm.Data.Context;
using KCrm.Logic.Services.Tags.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Logic.Services.Tags.Queries {
    public class GetTagWithRootQuery : AuthentiactedBaseRequest, IRequest<IList<TagWithRootDto>> {
        
    }

    public class GetTagWithRootQueryHandler : BaseHandler,
        IRequestHandler<GetTagWithRootQuery, IList<TagWithRootDto>> {

        private readonly TagContext _context;
        
        public GetTagWithRootQueryHandler(IMapper mapper, TagContext context) : base(mapper) {
            _context = context;
        }

        public async Task<IList<TagWithRootDto>> Handle(GetTagWithRootQuery request, CancellationToken cancellationToken) {
            var query = (from root in _context.TagGroups
                join tag in _context.Tags on root.Id equals tag.TagGroupId
                select new TagWithRootDto { Name = tag.Name, Root = root.Name, TagId = tag.Id.ToString ("D") });

            return await query.AsNoTracking ( ).ToListAsync ( cancellationToken);
           
        }
    } 
}
