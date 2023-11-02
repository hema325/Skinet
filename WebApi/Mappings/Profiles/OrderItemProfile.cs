using AutoMapper;
using Core.Entities.OrderAggregate;
using WebApi.AutoMapper.Resolvers;
using WebApi.Dtos.Respons;

namespace WebApi.Mappings.Profiles
{
    public class OrderItemProfile: Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dto=>dto.PictureUrl,opt=>opt.MapFrom<PictureUrlResolver>());
        }
    }
}
