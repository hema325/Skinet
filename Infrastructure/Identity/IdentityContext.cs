using Core.Entities.Identity;
using Infrastructure.Identity.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    internal class IdentityContext: IdentityDbContext<AppUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());
        }
    }
}
