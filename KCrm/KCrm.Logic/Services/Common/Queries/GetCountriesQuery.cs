using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KCrm.Data.GeoLocation;
using KCrm.Logic.Services.Common.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KCrm.Logic.Services.Common.Queries {
    
    //public 
    
    public class GetCountriesQuery : AuthentiactedBaseRequest, IRequest<IEnumerable<CountryDto>> {
        public string Query { get; set; }
    }

    public class GetCountriesQueryHandler : BaseHandler, IRequestHandler<GetCountriesQuery, IEnumerable<CountryDto>> {

        private readonly GeoLocationContext _geoLocationContext;

        public GetCountriesQueryHandler(IMapper mapper, GeoLocationContext geoLocationContext) : base (mapper) {
            _geoLocationContext = geoLocationContext;
        }

        public async Task<IEnumerable<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken) {
            var query = request.Query;

            var countries =
                _geoLocationContext.Countries.Where (x =>
                    string.IsNullOrWhiteSpace (query) ||
                    (x.Name.ToLower ( ).Contains (query.ToLower ( )) || x.Iso.ToLower ( ).Contains (query.ToLower ( ))));

            return await _mapper.ProjectTo<CountryDto> (countries).ToListAsync (cancellationToken);
        }
    }


}
