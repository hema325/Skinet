
namespace WebApi.Dtos.Respons
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DeliveryMethodDto DeliveryMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        public string Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
