namespace OrderManagement.Application.Abstractions.Dto
{
    public class OrderInvoiceProductDto
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Discount { get; set; }

        public decimal Amount { get; set; }
    }
}
