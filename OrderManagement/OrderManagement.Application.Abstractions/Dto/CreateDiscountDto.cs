namespace OrderManagement.Application.Abstractions.Dto
{
    public class CreateDiscountDto
    {
        public decimal? Percentage { get; set; }

        public int? Quantity { get; set; }
    }
}
