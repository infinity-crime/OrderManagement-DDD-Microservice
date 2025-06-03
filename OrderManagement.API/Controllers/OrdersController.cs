using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Models;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.Services;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            Guid orderId = await _orderService.CreateOrderAsync(request.CustomerId, request.ShippingAddress);

            return orderId != Guid.Empty
                ? Created($"/api/orders", new
                {
                    OrderId = orderId,
                    request.CustomerId,
                    request.ShippingAddress,
                    Date = DateTime.UtcNow
                })
                : BadRequest();
        }

        [HttpDelete]
        [Route("delete/{orderId}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid orderId)
        {
            return await _orderService.DeleteOrderAsync(orderId)
            ? NoContent()
            : ValidationProblem(title: "Not found this orderId", modelStateDictionary: ModelState);
        }

        [HttpPost]
        [Route("{orderId}/add-items")]
        public async Task<IActionResult> AddOrderItem([FromRoute] Guid orderId, [FromBody] CreateOrderItemRequest request)
        {
            try
            {
                await _orderService.AddItemToOrderAsync(orderId, request.ProductId, request.Quantity, request.UnitPrice);
                return Ok();
            }
            catch(OrderIdNotFoundException ex)
            {
                return NotFound(new
                {
                    Error = ex.Message,
                    Value = ex.Value
                });
            }
            catch(DomainException ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                });
            }
        }
    }
}
