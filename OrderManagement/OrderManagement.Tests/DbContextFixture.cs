using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Persistence;

namespace OrderManagement.Tests
{
    public class DbContextFixture : IDisposable
    {
        public RetailDbContext DbContext;

        public DbContextFixture()
        {
            var options = new DbContextOptionsBuilder<RetailDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            DbContext = new RetailDbContext(options);

            DbContext.Products.Add(new Product { ProductId = 1, Name = "Telefonas", Price = 500 });
            DbContext.Products.Add(new Product { ProductId = 2, Name = "Kompiuteris", Price = 1000 });
            DbContext.Products.Add(new Product { ProductId = 3, Name = "Televizorius", Price = 1500 });
            DbContext.Products.Add(new Product { ProductId = 4, Name = "PS5", Price = 400 });

            DbContext.Discounts.Add(new Discount { ProductId = 1, Percentage = 50, Quantity = 5 });
            DbContext.Discounts.Add(new Discount { ProductId = 2, Percentage = 20, Quantity = 3 });
            DbContext.Discounts.Add(new Discount { ProductId = 3, Percentage = 10, Quantity = 2 });

            DbContext.Orders.Add(new Order
            {
                OrderId = new Guid("326f879a-73c9-4dd9-a5a0-f515c94c7e58"),
                OrderProducts =
                [
                    new OrderProduct { ProductId = 1, Quantity = 5 },
                    new OrderProduct { ProductId = 2, Quantity = 10 }
                ]
            });

            DbContext.Orders.Add(new Order
            {
                OrderId = new Guid("e7c81abe-568b-43f0-9b9f-c4fc5041daaa"),
                OrderProducts =
                [
                    new OrderProduct { ProductId = 3, Quantity = 5 },
                ]
            });

            DbContext.SaveChanges();
        }

        public void Dispose() => DbContext?.Dispose();
    }
}
