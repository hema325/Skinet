using Core.Entities;
using Core.Entities.OrderAggregate;
using Infrastructure.Identity.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    internal class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options): base(options)  {   }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),typ => 
            typ != typeof(AppUserConfiguration) && typ != typeof(AddressConfiguration));
        }

        public DbSet<Brand> Brands { get; private set; }
        public DbSet<Category> Categories { get; private set; }
        public DbSet<Product> Products { get; private set; }
        public DbSet<Order> Orders { get; private set; }
        public DbSet<OrderItem> OrderItems { get; private set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; private set; }
    }
}
