namespace OrderManagement.Application.Abstractions.Dto
{
    public class CreateProductDto
    {
        public string Name { get; set; }

        public decimal? Price { get; set; }
    }
}
