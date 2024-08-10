using FluentValidation;
using Shared.Dtos;

namespace API.Validators
{
    public class StockItemForCreateValidator: AbstractValidator<StockItemForCreateDto>
    {
        public StockItemForCreateValidator()
        {
            RuleFor(si => si.StoreId)
                .NotNull().WithMessage("StoreId is required.")
                .NotEmpty().WithMessage("StoreId is not empty.");

            RuleFor(si => si.ProductId)
                .NotNull().WithMessage("ProductId is required.")
                .NotEmpty().WithMessage("ProductId is not empty.");

            RuleFor(si => si.Quantity)
                .NotNull().WithMessage("Quantity is required.");
        }
    }
}
