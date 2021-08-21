using System;

namespace Marble.Core.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event @event, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
            Data = data;
            User = user;
        }
        
        protected StoredEvent()
        {
        }

        public Guid Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}