using FluentValidation;
using OrderManagement.Application.Abstractions.Dto;

namespace OrderManagement.Application.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(p => p.Products)
                .NotEmpty()
                .WithMessage("Products list must not be empty.")
                .Must(p => p
                    .Select(op => op.ProductId)
                    .Distinct()
                    .Count() == p.Count)
                .WithMessage("Duplicate products are not allowed");

            RuleForEach(p => p.Products)
                .SetValidator(new CreateOrderProductValidator());
        }
    }
}
