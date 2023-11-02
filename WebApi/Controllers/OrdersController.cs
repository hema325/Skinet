using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Dtos.Request;
using WebApi.Dtos.Respons;
using WebApi.Errors;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/orders")]
    [Authorize]
    public class OrdersController : ApiControllerBase
    {
        private readonly IUnitOfWork _ufw;
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _ufw = unitOfWork;
            _orderRepo = unitOfWork.Repository<Order>();
            _mapper = mapper;
            _cacheService = cacheService;
        }

        [HttpGet("currentUserOrders")]
        public async Task<IActionResult> GetCurrentUserOrdersAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderRepo.GetListBySpecAsync(new GetUserOrdersSpec(userId));

            return Ok(_mapper.Map<OrderDto[]>(orders));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var order = await _orderRepo.GetBySpecAsync(new GetOrderByIdSpec(id));

            if (order == null)
                return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<OrderDto>(order));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderDto dto)
        {
            var productRepo = _ufw.Repository<Product>();

            var orderItems = new List<OrderItem>();
            foreach(var item in dto.Items)
            {
                var product = await productRepo.GetBySpecAsync(new GetProductsSpec(item.ProductId));

                if (product == null)
                    return BadRequest(new ApiResponse(400));

                orderItems.Add(new OrderItem
                {
                    Quantity = item.Quantity,
                    Name = product.Name,
                    Price = product.Price,
                    PictureUrl = product.PictureUrl,
                    CategoryName = product.Category.Name,
                    BrandName = product.Brand.Name
                });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var paymentKey = $"intent-{userId}";

            var paymentIntent = await _cacheService.GetAsync<PaymentIntent>(paymentKey);
            await _cacheService.DeleteAsync(paymentKey);

            if (paymentIntent == null)
                return BadRequest(new ApiResponse(404));

            var order = new Order 
            { 
                BuyerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
                OrderDate = DateTime.UtcNow,
                DeliveryMethodId = dto.DeliveryMethodId,
                Status = OrderStatus.Pending,
                ShippingAddress = _mapper.Map<ShippingAddress>(dto.ShippingAddress),
                Items = orderItems,
                PaymentIntentId = paymentIntent.Id
            };

            _orderRepo.Add(order);
            await _ufw.SaveChangesAsync();

            return Ok(order.Id);
        }
    }
}
