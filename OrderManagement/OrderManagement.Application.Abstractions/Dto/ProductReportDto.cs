namespace OrderManagement.Application.Abstractions.Dto
{
    public class ProductReportDto
    {
        public string Name { get; set; }

        public decimal Discount { get; set; }

        public int NumberOfOrders { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
