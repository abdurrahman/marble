using Marble.Domain.Models;
using Marble.Infrastructure.Data.Maps;
using Microsoft.EntityFrameworkCore;

namespace Marble.Infrastructure.Data.Context
{
    public class MarbleDbContext : DbContext
    {
        public MarbleDbContext(DbContextOptions<MarbleDbContext> options)
            :base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ProductMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}