using System;
using System.Collections.Generic;
using Marble.Core.Data;
using Marble.Domain.Models;

namespace Marble.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetPublishedProducts();

        Product GetByIdAsNoTracking(Guid id);
    }
}