namespace OrderManagement.Application.Abstractions.Dto
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }

        public ICollection<OrderProductDto> Products { get; set; }
    }
}
