using System;
using Marble.Core.Events;

namespace Marble.Domain.Events
{
    public class ProductUpdatedEvent : Event
    {
        public ProductUpdatedEvent(Guid id,
            string title,
            string description,
            decimal price,
            int stock,
            Guid categoryId)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public Guid CategoryId { get; set; }
    }
}