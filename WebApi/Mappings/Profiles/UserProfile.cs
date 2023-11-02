using AutoMapper;
using Core.Entities.Identity;
using WebApi.Dtos.Respons;

namespace WebApi.Mappings.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserDto>();
        }
    }
}
