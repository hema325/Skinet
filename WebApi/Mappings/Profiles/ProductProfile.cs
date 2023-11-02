using AutoMapper;
using Core.Entities;
using WebApi.AutoMapper.Resolvers;
using WebApi.Dtos.Respons;

namespace WebApi.Mapper.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dto=>dto.BrandName, opt=> opt.MapFrom(p=>p.Brand.Name))
                .ForMember(dto=>dto.CategoryName, opt=> opt.MapFrom(p=>p.Category.Name))
                .ForMember(dto=>dto.PictureUrl, opt=>opt.MapFrom<PictureUrlResolver>());
        }
    }
}
