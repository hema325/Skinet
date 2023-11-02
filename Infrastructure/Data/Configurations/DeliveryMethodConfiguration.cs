using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(dm => dm.Name).HasMaxLength(100);
            builder.Property(dm => dm.DeliveryTime).HasMaxLength(100);
            builder.Property(dm => dm.Price).HasPrecision(18,2);
        }
    }
}
