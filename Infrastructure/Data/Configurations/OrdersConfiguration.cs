using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, builder =>
            {
                builder.Property(a => a.Street).HasMaxLength(100);
                builder.Property(a => a.State).HasMaxLength(100);
                builder.Property(a => a.City).HasMaxLength(100);
                builder.Property(a => a.Zipcode).HasMaxLength(10);
            });

            builder.Property(o => o.Status).HasConversion(s => s.ToString(), s => Enum.Parse<OrderStatus>(s));
        }
    }
}
