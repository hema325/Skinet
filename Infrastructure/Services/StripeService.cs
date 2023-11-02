using Core.Interfaces.Services;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Stripe;

namespace Infrastructure.Services
{
    internal class StripeService: IPaymentService
    {
        private readonly StripeSettings _settings;
        public StripeService(IOptions<StripeSettings> settings)
        {
            _settings = settings.Value;
            StripeConfiguration.ApiKey = _settings.Secretkey;
        }

        public async  Task<Core.Models.PaymentIntent> CreatePaymentIntentAsync(long amount, string currency = "usd")
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount * 100,
                Currency = currency,
                PaymentMethodTypes = new List<string> 
                {
                    "card"
                }
            };
            var service = new PaymentIntentService();
            var intent = await service.CreateAsync(options);
            return new Core.Models.PaymentIntent
            {
                Id = intent.Id,
                ClientSecret = intent.ClientSecret
            };
        }

        public async Task UpdatePaymentIntentAsync(string intentId, long amount)
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = amount * 100
            };

            var service = new PaymentIntentService();
            await service.UpdateAsync(intentId, options);
        }


    }
}
