using Marble.Core.Data;
using Marble.Infrastructure.Data.Context;

namespace Marble.Infrastructure.Identity.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarbleDbContext _context;

        public UnitOfWork(MarbleDbContext context)
        {
            _context = context;
        }

        public bool Commit() => _context.SaveChanges() > 0;

        public void Dispose() => _context.Dispose();
    }
}