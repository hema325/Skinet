namespace Infrastructure.Settings
{
    internal class JwtSettings
    {
        public const string SectionName = "Jwt";

        public string Key { get; set; }
        public string Issuer { get; set; }
        public double ExpirationInHours { get; set; }
    }
}
