using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Persistence.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(k
                => new
                {
                    k.OrderId,
                    k.ProductId
                });

            builder.Property(p => p.Quantity)
                .IsRequired();

            builder.HasOne(p => p.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(fk => fk.ProductId);

            builder.HasOne(p => p.Order)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(fk => fk.OrderId);
        }
    }
}
