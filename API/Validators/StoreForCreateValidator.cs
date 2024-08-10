using FluentValidation;
using Shared.Dtos;

namespace API.Validators
{
    public class StoreForCreateValidator: AbstractValidator<StoreForCreateDto>
    {
        public StoreForCreateValidator() 
        {
            RuleFor(s => s.Name)
                .NotNull().WithMessage("Name is required.")
                .Length(min: 1, max: 256).WithMessage("Name must have length between 1 and 256.");

            RuleFor(e => e.Address)
                .NotNull().WithMessage("Address is required.")
                .Length(min: 1, max: 300).WithMessage("Address must have length between 1 and 300.");
        }
    }
}
