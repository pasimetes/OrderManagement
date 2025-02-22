using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.Abstractions.Dto;
using OrderManagement.Application.Abstractions.Services;
using OrderManagement.Application.Services;
using OrderManagement.Application.Validators;

namespace OrderManagement.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddTransient<IValidator<CreateProductDto>, CreateProductValidator>();
            services.AddTransient<IValidator<CreateOrderDto>, CreateOrderValidator>();
            services.AddTransient<IValidator<CreateDiscountDto>, CreateDiscountValidator>();
        }
    }
}
