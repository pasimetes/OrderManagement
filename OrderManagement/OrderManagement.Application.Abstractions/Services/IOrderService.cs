using OrderManagement.Application.Abstractions.Dto;
using OrderManagement.Application.Abstractions.Dto.Response;
namespace OrderManagement.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task<PagedResponse<OrderDto>> GetOrders(int pageNumber = 1, int pageSize = 20);

        Task<OrderInvoiceDto> GetInvoice(Guid orderId);

        Task<Guid> CreateOrder(IDictionary<int, int> orderProducts);
    }
}
