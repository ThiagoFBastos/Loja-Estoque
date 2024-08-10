using FluentValidation;
using Shared.Dtos;

namespace API.Validators
{
    public class ProductForUpdateValidator: AbstractValidator<ProductForUpdateDto>
    {
        public ProductForUpdateValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithMessage("Name is required.")
                .Length(min: 1, max: 256).WithMessage("Name have must length between 1 and 256.");

            RuleFor(p => p.Price)
                .NotNull().WithMessage("Price is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be non negative.")
                .PrecisionScale(precision: 15, scale: 2, ignoreTrailingZeros: false).WithMessage("Price have up two decimal places.");
        }
    }
}
