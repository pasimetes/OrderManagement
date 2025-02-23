using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Abstractions.Dto;
using OrderManagement.Application.Abstractions.Dto.Request;
using OrderManagement.Application.Abstractions.Services;

namespace OrderManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto order)
        {
            var orderProducts = order.Products.ToDictionary(k => k.ProductId, v => v.Quantity.GetValueOrDefault());

            await orderService.CreateOrder(orderProducts);

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] PagedRequest request)
        {
            var orders = await orderService.GetOrders(request.PageNumber, request.PageSize);

            return Ok(orders);
        }

        [HttpGet("{id}/invoice")]
        public async Task<IActionResult> GetOrderInvoice(Guid id)
        {
            var invoice = await orderService.GetInvoice(id);

            return Ok(invoice);
        }
    }
}
