using FluentValidation;
using OrderManagement.Application.Abstractions.Dto;

namespace OrderManagement.Application.Validators
{
    public class CreateOrderProductValidator : AbstractValidator<CreateOrderProductDto>
    {
        public CreateOrderProductValidator()
        {
            RuleFor(p => p.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is mandatory.");

            RuleFor(p => p.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");
        }
    }
}
