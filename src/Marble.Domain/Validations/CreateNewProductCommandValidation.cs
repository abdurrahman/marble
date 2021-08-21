using Marble.Domain.Commands;

namespace Marble.Domain.Validations
{
    public class CreateNewProductCommandValidation : ProductValidation<CreateNewProductCommand>
    {
        public CreateNewProductCommandValidation()
        {
            ValidateTitle();
            ValidateCategory();
            ValidatePrice();
        }
    }
}