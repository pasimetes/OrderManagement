using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Abstractions.Dto;
using OrderManagement.Application.Abstractions.Dto.Response;
using OrderManagement.Application.Abstractions.Services;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Persistence.Abstractions;

namespace OrderManagement.Application.Services
{
    public class OrderService(IApplicationDbContext applicationDbContext) : IOrderService
    {
        public async Task<PagedResponse<OrderDto>> GetOrders(int pageNumber = 0, int pageSize = 20)
        {
            var totalOrders = await applicationDbContext.Orders.CountAsync();

            var orders = await applicationDbContext.Orders
                .Select(p
                    => new OrderDto
                    {
                        OrderId = p.OrderId,
                        Products = p.OrderProducts
                            .Select(p
                                => new OrderProductDto
                                {
                                    Name = p.Product.Name,
                                    ProductId = p.ProductId,
                                    Quantity = p.Quantity
                                })
                            .ToList()
                    })
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<OrderDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Results = orders,
                TotalRecords = totalOrders
            };
        }

        public async Task<Guid> CreateOrder(IDictionary<int, int> orderProducts)
        {
            ThrowIf(() => orderProducts is null);
            ThrowIf(() => orderProducts.Count == 0);

            var order = new Order
            {
                OrderProducts = []
            };

            foreach (var orderProduct in orderProducts)
            {
                var productId = orderProduct.Key;
                var quantity = orderProduct.Value;

                var product = await applicationDbContext.Products.FindAsync(productId) ?? throw new NotFoundException($"Product with id {productId} was not found");

                order.OrderProducts.Add(
                    new OrderProduct
                    {
                        Order = order,
                        Product = product,
                        Quantity = quantity
                    });
            }

            applicationDbContext.Orders.Add(order);

            await applicationDbContext.SaveChangesAsync();

            return order.OrderId;
        }

        public async Task<OrderInvoiceDto> GetInvoice(Guid orderId)
        {
            var orderProducts = await applicationDbContext.OrderProducts
                .Where(p => p.OrderId == orderId)
                .Select(p
                    => new OrderInvoiceProductDto
                    {
                        Name = p.Product.Name,
                        Quantity = p.Quantity,

                        Discount = p.Product.Discount != null && p.Quantity >= p.Product.Discount.Quantity
                            ? p.Product.Discount.Percentage
                            : 0,

                        Amount = p.Quantity * p.Product.Price *
                            (p.Product.Discount != null && p.Quantity >= p.Product.Discount.Quantity
                                ? 1 - (p.Product.Discount.Percentage / 100)
                                : 1)
                    })
                .ToListAsync();

            if (orderProducts.Count == 0)
            {
                throw new NotFoundException($"Order with id {orderId} was not found.");
            }

            var invoice = new OrderInvoiceDto
            {
                Products = orderProducts,
                TotalAmount = orderProducts.Sum(p => p.Amount)
            };

            return invoice;
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
