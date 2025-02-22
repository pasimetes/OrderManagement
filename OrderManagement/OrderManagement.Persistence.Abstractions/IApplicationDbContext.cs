using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Persistence.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }

        DbSet<Order> Orders { get; }

        DbSet<Discount> Discounts { get; }

        DbSet<OrderProduct> OrderProducts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
