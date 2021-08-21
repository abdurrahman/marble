using System;
using System.Collections.Generic;
using System.Linq;
using Marble.Core.Events;
using Marble.Infrastructure.Data.Context;

namespace Marble.Infrastructure.Data.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreContext _context;

        public EventStoreRepository(EventStoreContext context)
        {
            _context = context;
        }

        public void Store(StoredEvent @event)
        {
            _context.StoredEvents.Add(@event);
            _context.SaveChanges();
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            return _context.StoredEvents.Where(x => x.AggregateId == aggregateId).ToList();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}