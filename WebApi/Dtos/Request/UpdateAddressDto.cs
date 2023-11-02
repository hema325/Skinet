using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.Request
{
    public class UpdateAddressDto
    {
        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(10)]
        public string Zipcode { get; set; }
    }
}
