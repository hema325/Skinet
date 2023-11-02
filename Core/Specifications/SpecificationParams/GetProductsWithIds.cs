using Core.Entities;
using Core.Specifications.Abstractions;

namespace Core.Specifications.SpecificationParams
{
    public class GetProductsWithIds: SpecificationBase<Product>
    {
        public GetProductsWithIds(IEnumerable<int> ids) : base(p => ids.Contains(p.Id))
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.Brand);
        }
    }
}
