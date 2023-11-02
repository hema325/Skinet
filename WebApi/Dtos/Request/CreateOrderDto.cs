namespace WebApi.Dtos.Request
{
    public class CreateOrderDto
    {
        public UpdateAddressDto ShippingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
        public BriefOrderItemDto[] Items { get; set; }
    }
}
