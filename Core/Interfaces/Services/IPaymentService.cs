using Core.Models;

namespace Core.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<PaymentIntent> CreatePaymentIntentAsync(long amount, string currency = "usd");
        Task UpdatePaymentIntentAsync(string intentId, long amount);
    }
}
