namespace OrderManagement.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
