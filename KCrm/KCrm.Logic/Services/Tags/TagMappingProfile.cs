using AutoMapper;
using KCrm.Core.Entity.Tags;
using KCrm.Logic.Services.Tags.Model;

namespace KCrm.Logic.Services.Tags {
    public class TagMappingProfile : Profile {
        public TagMappingProfile() {
            TagMapping ( );
        }

        private void TagMapping() {
            CreateMap<Tag, TagDto> ( ).ReverseMap ( );
        }
    }
}
