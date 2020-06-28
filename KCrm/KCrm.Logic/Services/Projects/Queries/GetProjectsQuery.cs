using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KCrm.Data.Context;
using KCrm.Logic.Services.Projects.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Logic.Services.Projects.Queries {
    public class GetProjectsQuery : AuthentiactedBaseRequest, IRequest<IEnumerable<ProjectDto>> {

    }

    public class GetProjectsQueryHandler : BaseHandler, IRequestHandler<GetProjectsQuery, IEnumerable<ProjectDto>> {
        private readonly ProjectContext _context;

        public GetProjectsQueryHandler(IMapper mapper, ProjectContext context) : base (mapper) {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ProjectDto>>
            Handle(GetProjectsQuery request, CancellationToken cancellationToken) {
            var result = await _context.Projects.ToListAsync (cancellationToken );
            return _mapper.Map<List<ProjectDto>> (result);
        }
    }
}
