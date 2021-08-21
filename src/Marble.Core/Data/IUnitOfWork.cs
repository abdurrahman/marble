using System;

namespace Marble.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}