namespace OrderManagement.Domain.Entities
{
    public class OrderProduct
    {
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
