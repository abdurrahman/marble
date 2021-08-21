using Marble.Domain.Commands;

namespace Marble.Domain.Validations
{
    public class UpdateProductCommandValidation : ProductValidation<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            ValidateId();
            ValidateTitle();
            ValidateStock();
            ValidateStock();
        }
    }
}