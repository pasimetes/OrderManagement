using OrderManagement.Application.Abstractions.Dto;
namespace OrderManagement.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task<ICollection<OrderDto>> GetOrders();

        Task<OrderInvoiceDto> GetInvoice(Guid orderId);

        Task<Guid> CreateOrder(IDictionary<int, int> orderProducts);
    }
}
