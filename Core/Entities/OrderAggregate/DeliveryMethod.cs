namespace Core.Entities.OrderAggregate
{
    public class DeliveryMethod : EntityBase
    {
        public string Name { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
