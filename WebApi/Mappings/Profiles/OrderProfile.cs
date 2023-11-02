using AutoMapper;
using Core.Entities.OrderAggregate;
using WebApi.Dtos.Respons;

namespace WebApi.Mappings.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dto=>dto.Status, opt => opt.MapFrom(o=>o.Status.ToString()));
        }
    }
}
