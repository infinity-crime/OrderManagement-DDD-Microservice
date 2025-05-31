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
            try
            {
                await _orderService.CreateOrderAsync(request.CustomerId, request.ShippingAddress);
            }
            catch (Exception ex)
            {
                return BadRequest(new {error = ex.Message});
            }

            return Created($"/api/orders", new
            {
                request.CustomerId,
                request.ShippingAddress,
                Date = DateTime.UtcNow
            });
        }
    }
}
