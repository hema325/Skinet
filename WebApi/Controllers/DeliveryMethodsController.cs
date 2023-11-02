using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Respons;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/deliveryMethods")]
    public class DeliveryMethodsController : ApiControllerBase
    {
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly IMapper _mapper;

        public DeliveryMethodsController(IGenericRepository<DeliveryMethod> deliveryMethods, IMapper mapper)
        {
            _deliveryMethodRepo = deliveryMethods;
            _mapper = mapper;
        }

        [HttpGet]
        [Cache]
        public async Task<IActionResult> GetAll()
        {
            var deliveryMethods = await _deliveryMethodRepo.GetAllAsync();
            return Ok(_mapper.Map<DeliveryMethodDto[]>(deliveryMethods));
        }
    }
}
