using System;
using Marble.Core.Commands;

namespace Marble.Domain.Commands
{
    public abstract class ProductCommand : Command
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public Guid CategoryId { get; set; }
    }
}