using System;
using System.Collections.Generic;
using Marble.Core;

namespace Marble.Domain.Models
{
    public class Category : Entity
    {
        public Category(Guid id,
            string name,
            int? parentId,
            int minimumQuantity)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            MinimumQuantity = minimumQuantity;
        }
        
        protected Category()
        {
        }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public int MinimumQuantity { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}