using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Tests.Service
{
    public class OrderServiceTests
    {
        private readonly OrderService _sut;
        private readonly DbContextFixture _fixture;

        public OrderServiceTests()
        {
            _fixture = new DbContextFixture();
            _sut = new OrderService(_fixture.DbContext);
        }

        [Fact]
        public async Task GetOrders_Returns_AllOrders()
        {
            // Act
            var orders = await _sut.GetOrders();

            // Assert
            Assert.NotNull(orders);
            Assert.True(orders.Results.Count >= 2);
        }

        [Fact]
        public async Task CreateOrder_ThrowsException_When_ProductsCollection_IsNullOrEmpty()
        {
            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(
                async () =>
                {
                    await _sut.CreateOrder(null);
                    await _sut.CreateOrder(new Dictionary<int, int>());
                });
        }

        [Fact]
        public async Task CreateOrder_Creates_Order()
        {
            // Act
            var orderId = await _sut.CreateOrder(new Dictionary<int, int>() { { 1, 100 } });

            // Assert
            var addedOrder = await _fixture.DbContext.Orders.FirstOrDefaultAsync(p => p.OrderId == orderId);

            Assert.NotNull(addedOrder);
            Assert.Single(addedOrder.OrderProducts);
            Assert.Equal(1, addedOrder.OrderProducts.Single().ProductId);
            Assert.Equal(100, addedOrder.OrderProducts.Single().Quantity);
        }

        [Fact]
        public async Task GetInvoice_ThrowsException_When_OrderIsNotFound()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                {
                    await _sut.GetInvoice(Guid.NewGuid());
                });
        }

        [Fact]
        public async Task GetInvoice_Returns_Invoice()
        {
            // Act
            var invoice = await _sut.GetInvoice(new Guid("326f879a-73c9-4dd9-a5a0-f515c94c7e58"));

            // Assert
            Assert.NotNull(invoice);
            Assert.Equal(9250, invoice.TotalAmount);
            Assert.Equal(2, invoice.Products.Count);
        }
    }
}
