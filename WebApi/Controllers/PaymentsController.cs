using Core.Entities.OrderAggregate;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;
using WebApi.Dtos.Request;
using WebApi.Errors;
using PaymentIntent = Core.Models.PaymentIntent;

namespace WebApi.Controllers
{
    [Route("/api/payments")]
    [Authorize]
    public class PaymentsController : ApiControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private readonly ILogger<PaymentsController> _looger;
        public PaymentsController(
            IPaymentService paymentService,
            IUnitOfWork unitOfWork,
            ICacheService cacheService,
            ILogger<PaymentsController> looger)
        {
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _looger = looger;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentIntent(CreatePaymentIntentDto dto)
        {
            //calculate order amount
            var productRepo = _unitOfWork.Repository<Core.Entities.Product>();
            var deliveryRepo = _unitOfWork.Repository<DeliveryMethod>();

            decimal amount = 0;
            foreach(var item in dto.Items)
            {
                var product =await productRepo.GetByIdAsync(item.ProductId);
                amount += product.Price * item.Quantity;
            }

            var delivery = await deliveryRepo.GetByIdAsync(dto.DeliveryMethodId);
            amount += delivery.Price;

            //update if payment intent already exists
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var paymentKey = $"intent-{userId}";

            var paymentIntent = await _cacheService.GetAsync<PaymentIntent>(paymentKey);
            if (paymentIntent == null)
            {
                paymentIntent = await _paymentService.CreatePaymentIntentAsync((long)amount);
                await _cacheService.SetAsync(paymentKey, paymentIntent);
            }
            else
                await _paymentService.UpdatePaymentIntentAsync(paymentIntent.Id, (long)amount);

            return Ok(paymentIntent);
        }

        [HttpPost("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> StripeWebhook()
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var intent = (Stripe.PaymentIntent)stripeEvent.Data.Object;
                var order = await orderRepo.GetBySpecAsync(new GetOrderByIntentIdSpec(intent.Id));

                if (order == null)
                    return NotFound(new ApiResponse(404));

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    order.Status = OrderStatus.PaymentReceived;
                }
                else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    order.Status = OrderStatus.PaymentFailed;
                }
                else
                {
                    _looger.LogWarning("Unhandled event type: {0}", stripeEvent.Type);
                }

                await _unitOfWork.SaveChangesAsync();

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(new ApiResponse(400));
            }
        }
    }
}
