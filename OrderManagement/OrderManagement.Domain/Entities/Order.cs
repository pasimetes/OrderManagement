namespace OrderManagement.Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
