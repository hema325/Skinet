using AutoMapper;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;
using WebApi.Dtos.Request;
using WebApi.Dtos.Respons;

namespace WebApi.Mappings.Profiles
{
    public class AddressProfile: Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<UpdateAddressDto, ShippingAddress>();
            CreateMap<ShippingAddress, AddressDto>();
            CreateMap<UpdateAddressDto, Address>();
        }
    }
}
