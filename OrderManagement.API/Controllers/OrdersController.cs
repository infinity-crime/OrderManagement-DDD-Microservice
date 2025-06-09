using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Models;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.Services;
using OrderManagement.Domain.ValueObjects;

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

        [HttpGet]
        [Route("{customerId}/all-orders")]
        public async Task<IActionResult> GetAllCustomerOrders([FromRoute] Guid customerId)
        {
            var orders = await _orderService.GetAllCustomerOrdersAsync(customerId);

            return orders.Count > 0
                ? Ok(orders)
                : NotFound(new
                {
                    Error = "Customer's Id is incorrect",
                    Id = customerId
                });
        }

        [HttpGet]
        [Route("{orderId}/all-items")]
        public async Task<IActionResult> GetAllOrderItems([FromRoute] Guid orderId)
        {
            try
            {
                var orderItems = await _orderService.GetAllOrderItemsAsync(orderId);

                return Ok(orderItems);
            }
            catch (OrderIdNotFoundException ex)
            {
                return NotFound(new
                {
                    Error = ex.Message,
                    Value = ex.Value
                });
            }
        }

        [HttpPost]
        [Route("order/{customerId}")]
        public async Task<IActionResult> CreateOrder([FromRoute] Guid customerId,
            [FromBody] CreateAddressRequest request)
        {
            try
            {
                var address = new ShippingAddress(request.Country, request.City, request.Street, request.PostCode);

                var orderId = await _orderService.CreateOrderAsync(customerId, address);

                return Created($"/api/Orders/create/order/{customerId}", orderId);
            }
            catch (DomainException ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("{orderId}/item")]
        public async Task<IActionResult> AddOrderItem([FromRoute] Guid orderId, [FromBody] CreateOrderItemRequest request)
        {
            try
            {
                var orderItemId = await _orderService.AddItemToOrderAsync(orderId, request.ProductId,
                    request.Quantity, request.UnitPrice);

                return Created($"/api/Orders/{orderId}/add-item", orderItemId);
            }
            catch (OrderIdNotFoundException ex)
            {
                return NotFound(new
                {
                    Error = ex.Message,
                    Value = ex.Value
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                });
            }
        }

        [HttpPut]
        [Route("{orderId}/address")]
        public async Task<IActionResult> UpdateOrderAddress([FromRoute] Guid orderId, 
            [FromBody] CreateAddressRequest request)
        {
            try
            {
                var address = new ShippingAddress(request.Country, request.City, request.Street, request.PostCode);

                await _orderService.ChangeAddressOrderAsync(orderId, address);

                return Ok(address);
            }
            catch (DomainException ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message
                });
            }
            catch (OrderIdNotFoundException ex)
            {
                return NotFound(new
                {
                    Error = ex.Message,
                    Value = ex.Value
                });
            }
        }

        [HttpDelete]
        [Route("{orderId}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid orderId)
        {
            try
            {
                await _orderService.DeleteOrderAsync(orderId);

                return NoContent();
            }
            catch (OrderIdNotFoundException ex)
            {
                return NotFound(new
                {
                    Error = ex.Message,
                    ex.Value
                });
            }
        }

        [HttpDelete]
        [Route("{orderId}/item/{itemId}")]
        public async Task<IActionResult> DeleteOrderItem([FromRoute] Guid orderId, [FromRoute] Guid itemId)
        {
            try
            {
                await _orderService.DeleteItemToOrderAsync(orderId, itemId);

                return NoContent();
            }
            catch (OrderIdNotFoundException ex)
            {
                return NotFound(new
                {
                    Error = ex.Message,
                    ex.Value
                });
            }
        }
    }
}
