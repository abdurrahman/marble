using System;
using System.Collections.Generic;
using Marble.Core.Events;

namespace Marble.Infrastructure.Data.Repositories
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent @event);

        IList<StoredEvent> All(Guid aggregateId);
    }
}