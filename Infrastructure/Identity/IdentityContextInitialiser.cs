using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure.Identity
{
    internal class IdentityContextInitialiser
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IdentityContext _context;
        public IdentityContextInitialiser(UserManager<AppUser> userManager, IdentityContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public void Initialise()
        {
            _context.Database.Migrate();
            Seed();
        }

        public void Seed()
        {
            if (!_userManager.Users.Any())
            {
                var users = JsonSerializer.Deserialize<AppUser[]>(File.ReadAllText("../Infrastructure/Identity/SeedData/Users.json"));
                foreach (var user in users)
                {
                    _userManager.CreateAsync(user, "Pa$$w0rd").GetAwaiter().GetResult();
                }
            }
        }
    }
}
