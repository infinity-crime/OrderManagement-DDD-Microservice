using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Models;
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
            Guid orderId = Guid.Empty;

            try
            {
                orderId = await _orderService.CreateOrderAsync(request.CustomerId, request.ShippingAddress);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = "404",
                    Errors = ex.Message
                });
            }

            return Created($"/api/orders", new
            {
                OrderId = orderId,
                request.CustomerId,
                request.ShippingAddress,
                Date = DateTime.UtcNow
            });
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
                return await _orderService.AddItemToOrderAsync(orderId, request.ProductId, request.Quantity,
                    request.UnitPrice)
                    ? Ok()
                    : ValidationProblem(title: "Not found this orderId", modelStateDictionary: ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
