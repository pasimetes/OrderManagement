using FluentValidation;
using OrderManagement.Application.Abstractions.Dto;

namespace OrderManagement.Application.Validators
{
    public class CreateDiscountValidator : AbstractValidator<CreateDiscountDto>
    {
        public CreateDiscountValidator()
        {
            RuleFor(p => p.Quantity)
                .NotEmpty()
                .WithMessage("Quantity is mandatory.")
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            RuleFor(p => p.Percentage)
                .NotEmpty()
                .WithMessage("Percentage is mandatory.")
                .GreaterThan(0)
                .WithMessage("Percentage must be greater than 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("Percentage must be lower than or equal to 100.");
        }
    }
}
