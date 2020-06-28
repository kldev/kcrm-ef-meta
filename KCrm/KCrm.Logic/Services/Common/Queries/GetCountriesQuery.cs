using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KCrm.Data.Context;
using KCrm.Logic.Services.Common.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Logic.Services.Common.Queries {
    public class GetCountriesQuery : AuthentiactedBaseRequest, IRequest<IEnumerable<CountryDto>> {
        public string Query { get; set; }
    }

    public class GetCountriesQueryHandler : BaseHandler, IRequestHandler<GetCountriesQuery, IEnumerable<CountryDto>> {

        private readonly CommonContext _commonContext;

        public GetCountriesQueryHandler(IMapper mapper, CommonContext commonContext) : base (mapper) {
            _commonContext = commonContext;
        }

        public async Task<IEnumerable<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken) {
            var query = request.Query;

            var countries =
                _commonContext.Countries.Where (x =>
                    string.IsNullOrWhiteSpace (query) ||
                    (x.Name.ToLower ( ).Contains (query.ToLower ( )) || x.Iso.ToLower ( ).Contains (query.ToLower ( ))));

            return await _mapper.ProjectTo<CountryDto> (countries).ToListAsync (cancellationToken);
        }
    }


}
