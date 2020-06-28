using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KCrm.Core.Model.Data;
using KCrm.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Logic.Services.Tags.Queries {
    public class GetTagAggregateCountQuery : AuthentiactedBaseRequest, IRequest<IList<GroupedCountModel>> {
        
    }

    public class GetTagAggregateCountHandlerQuery : BaseHandler,
        IRequestHandler<GetTagAggregateCountQuery, IList<GroupedCountModel>> {
        private readonly TagContext _context;

        public GetTagAggregateCountHandlerQuery(IMapper mapper, TagContext context) : base(mapper) {
            _context = context;
        }

        public async Task<IList<GroupedCountModel>> Handle(GetTagAggregateCountQuery request, CancellationToken cancellationToken) { var query = (from root in _context.TagGroups
                join tag in _context.Tags on root.Id equals tag.TagGroupId into leftJoin
                from subset in leftJoin.DefaultIfEmpty ( )
                let tagCount = root.Tags.Count
                group root by new { root.Id, root.Name }
                into g
                select new GroupedCountModel ( ) {
                    Count = (from x in _context.Tags where x.TagGroupId.Equals (g.Key.Id) select x.Id).Count ( ),
                    Name = g.Key.Name
                });


            return await query.AsNoTracking ( ).ToListAsync ( cancellationToken );
        }
    }
    
}
