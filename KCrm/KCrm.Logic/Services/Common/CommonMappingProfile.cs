using AutoMapper;
using KCrm.Core.Entity.GeoLocation;
using KCrm.Logic.Services.Common.Model;

namespace KCrm.Logic.Services.Common {
    public class CommonMappingProfile : Profile {

        public CommonMappingProfile() {
            CommonMapping ( );
        }

        private void CommonMapping() {
            CreateMap<CountryEntity, CountryDto> ( ).ReverseMap ( );
        }
    }
}
