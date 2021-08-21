using System;

namespace Marble.Domain.Exceptions
{
    public class ProductMustHaveACategoryException : Exception
    {
        public ProductMustHaveACategoryException()
        {
        }

        public ProductMustHaveACategoryException(string message)
            : base(message)
        {
        }
    }
}