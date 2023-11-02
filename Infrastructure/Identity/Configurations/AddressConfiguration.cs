using Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.Configurations
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.Street).HasMaxLength(100);
            builder.Property(a => a.State).HasMaxLength(100);
            builder.Property(a => a.City).HasMaxLength(100);
            builder.Property(a => a.Zipcode).HasMaxLength(10);
        }
    }
}
