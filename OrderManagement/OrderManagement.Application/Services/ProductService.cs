using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Abstractions.Dto;
using OrderManagement.Application.Abstractions.Dto.Response;
using OrderManagement.Application.Abstractions.Services;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Persistence.Abstractions;

namespace OrderManagement.Application.Services
{
    public class ProductService(IApplicationDbContext applicationDbContext) : IProductService
    {
        public async Task<int> CreateProduct(string name, decimal price)
        {
            ThrowIf(() => string.IsNullOrEmpty(name));
            ThrowIf(() => price < 0);

            var product = new Product
            {
                Name = name,
                Price = price
            };

            await applicationDbContext.Products.AddAsync(product);
            await applicationDbContext.SaveChangesAsync();

            return product.ProductId;
        }

        public async Task<PagedResponse<ProductDto>> SearchProducts(string searchQuery = "", int pageNumber = 0, int pageSize = 20)
        {
            var totalRecords = await applicationDbContext.Products.CountAsync();

            var products = await applicationDbContext.Products
                .Where(p => p.Name.Contains(searchQuery))
                .Select(p
                    => new ProductDto
                    {
                        Name = p.Name,
                        Price = p.Price,
                        ProductId = p.ProductId
                    })
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<ProductDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Results = products,
                TotalRecords = totalRecords
            };
        }

        public async Task ApplyDiscount(int productId, decimal discountPercentage, int minimumQuantity)
        {
            ThrowIf(() => discountPercentage < 0 || discountPercentage > 100);
            ThrowIf(() => minimumQuantity < 0);

            var product = await applicationDbContext.Products
                .Include(p => p.Discount)
                .FirstOrDefaultAsync(p => p.ProductId == productId) ?? throw new NotFoundException($"Product with id {productId} was not found.");

            if (product.Discount is null)
            {
                product.Discount = new Discount
                {
                    Percentage = discountPercentage,
                    Quantity = minimumQuantity
                };
                applicationDbContext.Discounts.Add(product.Discount);
            }
            else
            {
                product.Discount.Percentage = discountPercentage;
                product.Discount.Quantity = minimumQuantity;
            }

            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<ProductReportDto> GetDiscountedProductReport(int productId)
        {
            var discountedProductReport = await applicationDbContext.Products
                .Where(p => p.ProductId == productId && p.Discount != null)
                .Select(p => new
                {
                    ProductName = p.Name,
                    DiscountPercentage = p.Discount.Percentage,
                    Orders = p.OrderProducts
                        .Where(op => op.Quantity >= p.Discount.Quantity)
                        .Select(op
                            => new
                            {
                                op.Quantity,
                                TotalPrice = op.Quantity * p.Price * (1 - (p.Discount.Percentage / 100))
                            })
                        .ToList()
                })
                .FirstOrDefaultAsync() ?? throw new NotFoundException($"Discounted product with id {productId} was not found.");

            var report = new ProductReportDto
            {
                Name = discountedProductReport.ProductName,
                Discount = discountedProductReport.DiscountPercentage,
                NumberOfOrders = discountedProductReport.Orders.Count,
                TotalAmount = discountedProductReport.Orders.Sum(o => o.TotalPrice)
            };

            return report;
        }

        private static void ThrowIf(Func<bool> predicate, string errorMessage = "")
        {
            if (predicate())
            {
                throw new BusinessException(errorMessage);
            }
        }
    }
}
