using Core.Entities;
using Core.Specifications.Abstractions;

namespace Core.Specifications
{
    public class GetProductsSpec: SpecificationBase<Product>
    {
        public GetProductsSpec(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.Brand);
        }

    }
}
