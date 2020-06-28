using AutoMapper;
using KCrm.Core.Entity.Common;
using KCrm.Logic.Services.Common.Model;

namespace KCrm.Logic.Services.Common {
    public class CommonMappingProfile : Profile {

        public CommonMappingProfile() {
            CommonMapping ( );
        }

        private void CommonMapping() {
            CreateMap<Country, CountryDto> ( ).ReverseMap ( );
        }
    }
}
