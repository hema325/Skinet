using AutoMapper;
using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/categories")]
    public class CategoriesController : ApiControllerBase
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public CategoriesController(IGenericRepository<Category> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Cache]
        public async Task<IActionResult> GetAllProducts()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(_mapper.Map<Category[]>(categories));
        }
    }
}
