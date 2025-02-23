using FluentValidation;
using OrderManagement.Application.Abstractions.Dto.Request;

namespace OrderManagement.Application.Validators
{
    public class PagedRequestValidator : AbstractValidator<PagedRequest>
    {
        public PagedRequestValidator()
        {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Page number must be greater than or equal to 0.");

            RuleFor(p => p.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.");
        }
    }
}
