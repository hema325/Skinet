using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure.Data
{
    internal class StoreContextInitialiser
    {
        private readonly StoreContext _context;

        public StoreContextInitialiser(StoreContext context)
        {
            _context = context;
        }

        public void Initialise()
        {
           _context.Database.Migrate();
           Seed();
        }

        private void Seed()
        {
            if(!_context.Brands.Any())
            {
                var brands = JsonSerializer.Deserialize<Brand[]>(File.ReadAllText("../Infrastructure/Data/SeedData/Brands.json"));
                _context.Brands.AddRange(brands!);
            }

            if (!_context.Categories.Any())
            {
                var categories = JsonSerializer.Deserialize<Category[]>(File.ReadAllText("../Infrastructure/Data/SeedData/Categories.json"));
                _context.Categories.AddRange(categories!);
            }

            if (!_context.Products.Any())
            {
                var products = JsonSerializer.Deserialize<Product[]>(File.ReadAllText("../Infrastructure/Data/SeedData/Products.json"));
                _context.Products.AddRange(products!);
            }

            if(!_context.DeliveryMethods.Any())
            {
                var deliveryMethods = JsonSerializer.Deserialize<DeliveryMethod[]>(File.ReadAllText("../Infrastructure/Data/SeedData/DeliveryMethods.json"));
                _context.DeliveryMethods.AddRange(deliveryMethods!);
            }

            if(_context.ChangeTracker.HasChanges()) _context.SaveChanges();
        }
    }
}
