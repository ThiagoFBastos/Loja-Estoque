using FluentValidation;
using Shared.Dtos;

namespace API.Validators
{
    public class StockItemForUpdateValidator: AbstractValidator<StockItemForUpdateDto>
    {
        public StockItemForUpdateValidator()
        {
            RuleFor(si => si.Quantity)
                .NotNull().WithMessage("Quantity is required.");
        }
    }
}
