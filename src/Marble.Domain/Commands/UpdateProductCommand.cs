using System;
using Marble.Domain.Validations;

namespace Marble.Domain.Commands
{
    public class UpdateProductCommand : ProductCommand
    {
        public UpdateProductCommand(Guid id, string title, string description, 
            decimal price, int stockQuantity, Guid categoryId)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Stock = stockQuantity;
            CategoryId = categoryId;
        }

        public UpdateProductCommand()
        {
        }
        
        public override bool IsValid()
        {
            ValidationResult = new UpdateProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}