using System;
using FluentValidation;
using Marble.Domain.Commands;

namespace Marble.Domain.Validations
{
    public abstract class ProductValidation<T> : AbstractValidator<T> where T : ProductCommand
    {
        protected void ValidateTitle()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title field is required")
                .MaximumLength(200).WithMessage("Title field should less than 200 character");
        }

        protected void ValidatePrice()
        {
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The price can't be negative value");
        }

        protected void ValidateCategory()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Category must be set");
        }

        // protected void ValidateMinimumStockQuantity()
        // {
        //     RuleFor(x => x.Stock)
        //         .Custom
        // }

        protected void ValidateStock()
        {
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The stock can't be negative value");
        }

        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}