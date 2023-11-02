namespace Core.Specifications.SpecificationParams
{
    public class ProductSpecParams
    {
        public string? Search { get; set; }
        public string? Sort { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
