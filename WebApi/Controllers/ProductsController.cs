using AutoMapper;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Specifications;
using Core.Specifications.SpecificationParams;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Respons;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/products")]
    public class ProductsController : ApiControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Cache]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepo.GetBySpecAsync(new GetProductsSpec(id));

            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpGet]
        [Cache]
        public async Task<IActionResult> GetProducts([FromQuery] ProductSpecParams prms)
        {
            var products = await _productRepo.GetListBySpecAsync(new GetProductsFilteredSpec(prms));
            var totalProductsCount = await _productRepo.CountBySpecAsync(new GetProductsCountSpec(prms));
            
            return Ok(new PaginatedListDto<ProductDto>(_mapper.Map<IReadOnlyCollection<ProductDto>>(products),
                                                       totalProductsCount,
                                                       prms.PageNumber,
                                                       prms.PageSize));
        }

        [HttpGet("findByIds")]
        [Cache]
        public async Task<IActionResult> GetProductsByIds([FromQuery] int[] ids)
        {
            var products = await _productRepo.GetListBySpecAsync(new GetProductsWithIds(ids));
            return Ok(_mapper.Map<ProductDto[]>(products));
        }
    }
}
