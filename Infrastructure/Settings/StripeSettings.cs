namespace Infrastructure.Settings
{
    internal class StripeSettings
    {
        public const string SectionName = "Stripe";
        public string Publishablekey { get; set; }
        public string Secretkey { get; set; }
    }
}
