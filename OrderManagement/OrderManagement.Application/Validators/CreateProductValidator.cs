using FluentValidation;
using OrderManagement.Application.Abstractions.Dto;

namespace OrderManagement.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty();

            RuleFor(p => p.Price)
                .NotEmpty()
                .WithMessage("Price is mandatory.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0.");
        }
    }
}
