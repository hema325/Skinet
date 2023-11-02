namespace WebApi.Dtos.Request
{
    public class CreatePaymentIntentDto
    {
        public int DeliveryMethodId { get; set; }
        public BriefOrderItemDto[] Items { get; set; }
    }
}
