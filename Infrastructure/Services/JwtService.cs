using Core.Entities.Identity;
using Core.Interfaces.Services;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    internal class JwtService: ITokenService
    {
        private readonly JwtSettings _settings;

        public JwtService(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }


        public string CreateToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var creadentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_settings.ExpirationInHours),
                issuer: _settings.Issuer,
                signingCredentials: creadentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}
