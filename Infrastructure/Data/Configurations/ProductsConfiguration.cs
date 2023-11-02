using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.HasIndex(p => p.Name).IsUnique();

            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.Property(p => p.PictureUrl).HasMaxLength(250);

            builder.HasOne(p => p.Brand).WithMany().HasForeignKey(p => p.BrandId);
            builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
        }
    }
}
