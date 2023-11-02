using Core.Entities.OrderAggregate;
using Core.Specifications.Abstractions;

namespace Core.Specifications
{
    public class GetOrderByIdSpec: SpecificationBase<Order>
    {
        public GetOrderByIdSpec(int id): base(o => o.Id == id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }

    }
}
