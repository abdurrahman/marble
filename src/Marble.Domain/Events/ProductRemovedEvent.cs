using System;
using Marble.Core.Events;

namespace Marble.Domain.Events
{
    public class ProductRemovedEvent : Event
    {
        public ProductRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}