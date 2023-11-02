namespace Core.Entities.OrderAggregate
{
    public class Order: EntityBase
    {
        public string BuyerId { get; set; }
        public DateTime OrderDate { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public string? PaymentIntentId { get; set; }

        //derived attributes
        public decimal SubTotal => Items.Sum(o => o.Price);
        public decimal Total => SubTotal + DeliveryMethod?.Price ?? 0;
    }
}
