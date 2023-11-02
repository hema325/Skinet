using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos.Requests
{
    public class RegisterDto
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{6,}",
            ErrorMessage = "Password must be at leaset 6 letters containing upper/lower case, numbers and special cases")]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
