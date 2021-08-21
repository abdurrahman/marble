using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marble.Domain.Models;
using Marble.Infrastructure.Data.Context;

namespace Marble.Infrastructure.Data
{
    public static class DbContextSeed
    {
        public static async Task SeedAsync(MarbleDbContext context)
        {
            await SeedCategories(context);
            await SeedProducts(context);
        }

        private static async Task SeedProducts(MarbleDbContext context)
        {
            if (context.Products.Any()) return;

            var category = context.Categories.FirstOrDefault();
            
            var productList = new List<Product>
            {
                new Product(
                    Guid.NewGuid(),
                    "ZOTAC Gaming GeForce RTX 3060",
                    "12GB 192-bit GDDR6, 15 Gbps, PCIE 4.0; Boost Clock 1807 MHz",
                    6712.63m,
                    20,
                    category.Id
                ),
                new Product(
                    Guid.NewGuid(),
                    "Razer Huntsman V2 Analog Gaming Keyboard",
                    "Razer Analog Optical Switches: Set a desired actuation point to suit your play style",
                    2206,
                    15,
                    category.Id
                ),
                new Product(
                    Guid.NewGuid(),
                    "PowerColor Red Devil AMD Radeon RX 6800 XT",
                    "Gaming Graphics Card with 16GB GDDR6 Memory, Powered by AMD RDNA 2, Raytracing, " +
                    "PCI Express 4.0, HDMI 2.1, AMD Infinity Cache",
                    12246.70m,
                    50,
                    category.Id
                ),
                new Product(
                    Guid.NewGuid(),
                    "MSI Gaming GeForce GTX 1650 4GB GDDR6",
                    "The GeForce GTX 1650 is a budget-friendly gaming graphics card that turns you PC into a gaming machine.",
                    4135.21m,
                    45,
                    category.Id
                ),
                new Product(
                    Guid.NewGuid(),
                    "ASUS GeForce GTX 1050 Ti 4GB",
                    "owered by NVIDIA Pascal, ASUS Phoenix Series GTX 1050Ti delivers cool performance...",
                    2406.65m,
                    30,
                    category.Id
                ),
            };
            
            productList.ForEach(product =>
            {
                product.IsPublished = product.Stock >= category.MinimumQuantity;
                context.Products.Add(product);
            });
            await context.SaveChangesAsync();
        }

        private static async Task SeedCategories(MarbleDbContext context)
        {
            if (context.Categories.Any()) return;
            var categoryList = new List<Category>()
            {
                new Category(Guid.NewGuid(),
                    "Graphics Cards",
                    null,
                    20)
            };

            categoryList.ForEach(category =>
            {
                context.Categories.Add(category);
            });
            await context.SaveChangesAsync();
        }
    }
}