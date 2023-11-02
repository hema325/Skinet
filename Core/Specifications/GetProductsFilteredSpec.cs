using Core.Entities;
using Core.Specifications.Abstractions;
using Core.Specifications.SpecificationParams;

namespace Core.Specifications
{
    public class GetProductsFilteredSpec: SpecificationBase<Product>
    {
        public GetProductsFilteredSpec(ProductSpecParams prms) : base(p => (string.IsNullOrEmpty(prms.Search) || p.Name.StartsWith(prms.Search)) &&
        (!prms.CategoryId.HasValue || prms.CategoryId == p.CategoryId) &&
        (!prms.BrandId.HasValue || prms.BrandId == p.BrandId))
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.Brand);
            ApplyPaging((prms.PageNumber - 1) * prms.PageSize, prms.PageSize);

            switch (prms.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
        }
    }
}
