using System;

namespace Marble.Application.ViewModels
{
    public class ProductCreateModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public Guid CategoryId { get; set; }
    }
}