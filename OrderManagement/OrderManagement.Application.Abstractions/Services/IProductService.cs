using OrderManagement.Application.Abstractions.Dto;

namespace OrderManagement.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductDto>> SearchProducts(string searchQuery = "");

        Task<int> CreateProduct(string name, decimal price);

        Task ApplyDiscount(int productId, decimal discountPercentage, int minimumQuantity);

        Task<ProductReportDto> GetDiscountedProductReport(int productId);
    }
}
