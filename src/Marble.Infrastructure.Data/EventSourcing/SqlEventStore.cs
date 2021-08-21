using System.Text.Json;
using Marble.Core.Data;
using Marble.Core.Events;
using Marble.Infrastructure.Data.Repositories;

namespace Marble.Infrastructure.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IUser _user;

        public SqlEventStore(IEventStoreRepository eventStoreRepository, IUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public void Save<T>(T @event) where T : Event
        {
            var serializedData = JsonSerializer.Serialize(@event);

            var storedEvent = new StoredEvent(@event, serializedData, _user.Name);
            _eventStoreRepository.Store(storedEvent);
        }
    }
}