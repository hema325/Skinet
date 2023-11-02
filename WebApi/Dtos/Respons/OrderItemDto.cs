namespace WebApi.Dtos.Respons
{
    public class OrderItemDto
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
    }
}
