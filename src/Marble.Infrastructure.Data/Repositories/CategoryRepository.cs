using Marble.Domain.Models;
using Marble.Domain.Repositories;
using Marble.Infrastructure.Data.Context;

namespace Marble.Infrastructure.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MarbleDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}