using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace OrderManagement.WebApi.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                var response = new
                {
                    Message = "One or more validation errors occurred.",
                    Errors = errors
                };

                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
