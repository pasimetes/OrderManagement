namespace OrderManagement.Application.Abstractions.Dto
{
    public class OrderInvoiceDto
    {
        public ICollection<OrderInvoiceProductDto> Products { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
