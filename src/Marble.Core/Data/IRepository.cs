using System;
using System.Linq;
using System.Threading.Tasks;

namespace Marble.Core.Data
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : IDisposable 
        where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(ISpecification<TEntity> spec);

        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);

        Task<int> SaveChangesAsync();
    }
}