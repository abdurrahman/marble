using System;
using System.Collections.Generic;
using System.Linq;
using Marble.Domain.Models;
using Marble.Domain.Repositories;
using Marble.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Marble.Infrastructure.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MarbleDbContext dbContext) 
            : base(dbContext)
        {
        }

        public IEnumerable<Product> GetPublishedProducts()
        {
            return _dbSet.AsNoTracking().Where(x => x.IsPublished).AsEnumerable();
        }

        public Product GetByIdAsNoTracking(Guid id)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
    }
}