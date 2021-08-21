using System;
using System.Linq;
using System.Threading.Tasks;
using Marble.Core.Data;
using Marble.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Marble.Infrastructure.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where  TEntity : class
    {
        protected readonly MarbleDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(MarbleDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual IQueryable<TEntity> GetAll(ISpecification<TEntity> spec)
            => ApplySpecification(spec);

        public virtual void Delete(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
        
        public virtual void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), spec);
        }
    }
}