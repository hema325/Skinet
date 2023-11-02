using Core.Entities.OrderAggregate;
using Core.Specifications.Abstractions;

namespace Core.Specifications
{
    public class GetOrderByIntentIdSpec: SpecificationBase<Order>
    {
        public GetOrderByIntentIdSpec(string intentId): base(o=>o.PaymentIntentId == intentId)
        {
            
        }
    }
}
