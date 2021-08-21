using System;
using Marble.Domain.Validations;

namespace Marble.Domain.Commands
{
    public class CreateNewProductCommand : ProductCommand
    {
        public CreateNewProductCommand(string title,
            string description,
            decimal price,
            int stockQuantity,
            Guid categoryId)
        {
            Title = title;
            Description = description;
            Price = price;
            Stock = stockQuantity;
            CategoryId = categoryId;
        }

        public CreateNewProductCommand()
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateNewProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}