using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Persistence.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(k => k.DiscountId);

            builder.Property(p => p.Percentage)
                .IsRequired();

            builder.Property(p => p.Quantity)
                .IsRequired();

            builder.HasOne(p => p.Product)
                .WithOne(p => p.Discount)
                .HasForeignKey<Discount>(fk => fk.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
