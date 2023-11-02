namespace WebApi.Dtos.Respons
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
    }
}
