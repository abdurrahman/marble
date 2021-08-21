using System;
using Marble.Core;

namespace Marble.Domain.Models
{
    public class Product : Entity
    {
        public Product(Guid id,
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
            CreatedAt = DateTime.UtcNow;
        }
        
        protected Product()
        {
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsPublished { get; set; }

        public DateTime CreatedAt { get; set; }
        public virtual Category Category { get; set; }
    }
}