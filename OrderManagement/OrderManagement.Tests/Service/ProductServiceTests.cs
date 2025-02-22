using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Tests.Service
{
    public class ProductServiceTests
    {
        private readonly ProductService _sut;
        private readonly DbContextFixture _fixture;

        public ProductServiceTests()
        {
            _fixture = new DbContextFixture();
            _sut = new ProductService(_fixture.DbContext);
        }

        [Fact]
        public async Task SearchProducts_Returns_AllProducts()
        {
            // Act
            var products = await _sut.SearchProducts();

            // Assert
            Assert.NotNull(products);
            Assert.Contains(products.Results, p => p.Name == "Telefonas");
            Assert.Contains(products.Results, p => p.Name == "Kompiuteris");
            Assert.Contains(products.Results, p => p.Name == "Televizorius");
        }

        [Fact]
        public async Task SearchProducts_Returns_FilteredProducts()
        {
            // Act
            var products = await _sut.SearchProducts("Tel");

            // Assert
            Assert.NotNull(products);
            Assert.Contains(products.Results, p => p.Name == "Telefonas");
            Assert.Contains(products.Results, p => p.Name == "Televizorius");
            Assert.DoesNotContain(products.Results, p => p.Name == "Kompiuteris");
        }

        [Fact]
        public async Task CreateProduct_ThrowsException_When_PriceIsBelowZero()
        {
            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(
                async () =>
                {
                    await _sut.CreateProduct("Knyga", -1m);
                });
        }

        [Fact]
        public async Task CreateProduct_ThrowsException_When_NameIsNullOrEmpty()
        {
            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(
                async () =>
                {
                    await _sut.CreateProduct("", 1);
                });
        }

        [Fact]
        public async Task CreateProduct_Creates_Product()
        {
            // Act
            await _sut.CreateProduct("Knyga", 1m);

            // Assert
            var product = await _fixture.DbContext.Products.FirstOrDefaultAsync(p => p.Name == "Knyga");

            Assert.NotNull(product);
            Assert.Equal("Knyga", product.Name);
            Assert.Equal(1m, product.Price);
        }

        [Fact]
        public async Task GetDiscountedProductReport_ThrowsException_WhenDiscountedProductDoesntExist()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                {
                    await _sut.GetDiscountedProductReport(500);
                });
        }

        [Fact]
        public async Task GetDiscountedProductReport_Returns_ReportData()
        {
            // Act
            var report = await _sut.GetDiscountedProductReport(1);

            // Assert
            Assert.NotNull(report);
            Assert.Equal("Telefonas", report.Name);
            Assert.Equal(50, report.Discount);
            Assert.Equal(1, report.NumberOfOrders);
            Assert.Equal(1250, report.TotalAmount);
        }

        [Fact]
        public async Task ApplyDiscount_ThrowsException_When_ProductNotFound()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(
                async () =>
                {
                    await _sut.ApplyDiscount(1000, 20, 1);
                });
        }

        [Fact]
        public async Task ApplyDiscount_ThrowsException_When_DiscountPercentageIsIncorrect()
        {
            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(
                async () =>
                {
                    await _sut.ApplyDiscount(1, -1, 1);
                    await _sut.ApplyDiscount(1, 101, 1);

                });
        }

        [Fact]
        public async Task ApplyDiscount_ThrowsException_When_QuantityIsNegative()
        {
            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(
                async () =>
                {
                    await _sut.ApplyDiscount(1, 20, -1);
                });
        }

        [Fact]
        public async Task ApplyDiscount_Creates_Discount()
        {
            // Act
            await _sut.ApplyDiscount(4, 50, 5);

            // Assert
            var discount = await _fixture.DbContext.Discounts.FirstOrDefaultAsync(p => p.ProductId == 4);

            Assert.NotNull(discount);
            Assert.Equal(50, discount.Percentage);
            Assert.Equal(5, discount.Quantity);
        }

        [Fact]
        public async Task ApplyDiscount_Updates_Discount()
        {
            // Act
            await _sut.ApplyDiscount(1, 99, 10);

            // Assert
            var discount = await _fixture.DbContext.Discounts.FirstOrDefaultAsync(p => p.ProductId == 1);

            Assert.NotNull(discount);
            Assert.Equal(99, discount.Percentage);
            Assert.Equal(10, discount.Quantity);
        }
    }
}
