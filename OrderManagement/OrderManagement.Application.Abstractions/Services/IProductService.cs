using OrderManagement.Application.Abstractions.Dto;
using OrderManagement.Application.Abstractions.Dto.Response;

namespace OrderManagement.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<PagedResponse<ProductDto>> SearchProducts(string searchQuery = "", int pageNumber = 0, int pageSize = 20);

        Task<int> CreateProduct(string name, decimal price);

        Task ApplyDiscount(int productId, decimal discountPercentage, int minimumQuantity);

        Task<ProductReportDto> GetDiscountedProductReport(int productId);
    }
}
