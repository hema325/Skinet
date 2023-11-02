using AutoMapper;
using Core.Entities.OrderAggregate;
using WebApi.Dtos.Respons;

namespace WebApi.Mappings.Profiles
{
    public class DeliveryMethodProfile: Profile
    {
        public DeliveryMethodProfile()
        {
            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    }
}
