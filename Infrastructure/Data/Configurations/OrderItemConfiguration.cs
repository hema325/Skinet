using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100);

            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.Property(p => p.PictureUrl).HasMaxLength(250);

            builder.Property(p => p.CategoryName).HasMaxLength(100);
            builder.Property(p => p.BrandName).HasMaxLength(100);
        }
    }
}
