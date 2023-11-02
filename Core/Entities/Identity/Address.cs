namespace Core.Entities.Identity
{
    public class Address: EntityBase
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string AppUserId { get; set; }
    }
}
