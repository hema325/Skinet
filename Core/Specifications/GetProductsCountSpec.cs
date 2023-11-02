using Core.Entities;
using Core.Specifications.Abstractions;
using Core.Specifications.SpecificationParams;

namespace Core.Specifications
{
    public class GetProductsCountSpec: SpecificationBase<Product>
    {
        public GetProductsCountSpec(ProductSpecParams prms) : base(p => (string.IsNullOrEmpty(prms.Search) || p.Name.StartsWith(prms.Search)) &&
        (!prms.CategoryId.HasValue || prms.CategoryId == p.CategoryId) &&
        (!prms.BrandId.HasValue || prms.BrandId == p.BrandId))
        { 
        }
    }
}
