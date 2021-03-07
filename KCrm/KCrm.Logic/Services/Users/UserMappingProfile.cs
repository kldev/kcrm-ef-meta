using AutoMapper;
using KCrm.Core.Entity.Users;
using KCrm.Logic.Services.Users.Model;

namespace KCrm.Logic.Services.Users {
    public class UserMappingProfile : Profile {
        public UserMappingProfile() {
            UserMapping ( );
        }

        private void UserMapping() {
            CreateMap<UserAccountEntity, UserInfoDto> ( ).ConstructUsing (x => new UserInfoDto ( ) {
                Email = x.Email,
                Name = x.Name,
                LastName = x.LastName
            });
        }
    }
}
