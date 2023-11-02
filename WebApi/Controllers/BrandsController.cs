using AutoMapper;
using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Respons;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/brands")]
    public class BrandsController : ApiControllerBase
    {
        private readonly IGenericRepository<Brand> _brandRepo;
        private readonly IMapper _mapper;

        public BrandsController(IGenericRepository<Brand> brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Cache]
        public async Task<IActionResult> GetAllAsync()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(_mapper.Map<BrandDto[]>(brands));
        }
    }
}
