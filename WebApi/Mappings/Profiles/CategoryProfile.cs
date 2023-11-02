using AutoMapper;
using Core.Entities;
using WebApi.Dtos.Respons;

namespace WebApi.Mapper.Profiles
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
