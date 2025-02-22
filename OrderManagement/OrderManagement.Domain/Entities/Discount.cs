namespace OrderManagement.Domain.Entities
{
    public class Discount
    {
        public int DiscountId { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public decimal Percentage { get; set; }

        public int Quantity { get; set; }
    }
}
