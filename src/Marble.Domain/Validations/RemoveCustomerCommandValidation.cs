using Marble.Domain.Commands;

namespace Marble.Domain.Validations
{
    public class RemoveCustomerCommandValidation : ProductValidation<RemoveProductCommand>
    {
        public RemoveCustomerCommandValidation()
        {
            ValidateId();
        }
    }
}