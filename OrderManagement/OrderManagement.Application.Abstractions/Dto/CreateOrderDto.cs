namespace OrderManagement.Application.Abstractions.Dto
{
    public class CreateOrderDto
    {
        public ICollection<CreateOrderProductDto> Products { get; set; }
    }
}
