using AutoMapper;
using Core.Entities;
using WebApi.Dtos.Respons;

namespace WebApi.Mapper.Profiles
{
    public class BrandProfile: Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDto>();   
        }
    }
}
