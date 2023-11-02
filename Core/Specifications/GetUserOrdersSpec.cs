using Core.Entities.OrderAggregate;
using Core.Specifications.Abstractions;

namespace Core.Specifications
{
    public class GetUserOrdersSpec: SpecificationBase<Order>
    {
        public GetUserOrdersSpec(string userId) : base(o => o.BuyerId == userId)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
            AddOrderByDescending(o => o.OrderDate);
        }
    }
}
