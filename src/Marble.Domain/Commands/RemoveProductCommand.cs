using System;
using Marble.Domain.Validations;

namespace Marble.Domain.Commands
{
    public class RemoveProductCommand : ProductCommand
    {
        public RemoveProductCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RemoveCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}