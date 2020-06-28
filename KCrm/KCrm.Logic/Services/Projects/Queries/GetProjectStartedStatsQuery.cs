using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KCrm.Data.Aegis;
using KCrm.Logic.Services.Projects.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Logic.Services.Projects.Queries {
    public class GetProjectStartedStatsQuery : AuthentiactedBaseRequest, IRequest<IEnumerable<ProjectStartedStatDto>> {
        public int Year { get; set; } = 2020;
    }

    public class GetProjectStartedStatsQueryHandler : BaseHandler,
        IRequestHandler<GetProjectStartedStatsQuery, IEnumerable<ProjectStartedStatDto>> {
        private readonly AegisContext _aegisDb;


        public GetProjectStartedStatsQueryHandler(IMapper mapper, AegisContext aegisDb) : base (mapper) {
            _aegisDb = aegisDb;
        }

        public async Task<IEnumerable<ProjectStartedStatDto>> Handle(GetProjectStartedStatsQuery request,
            CancellationToken cancellationToken) {
            var result = await _aegisDb.ProjectStartedStats.Where (x => x.Year == request.Year.ToString ( ))
                .ToListAsync (cancellationToken);
            return _mapper.Map<List<ProjectStartedStatDto>> (result);
        }
    }
}
