using AvalonMozi.Application.Orders.Dto;
using AvalonMozi.Application.Orders.Services;
using AvalonMozi.Factories.OrderFactories.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvalonMozi.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN,EMPLOYEE")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("NewOrder")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> AddNewOrder(OrderRequestDto order)
        {
            var result = await _orderService.ProcessOrderRequest(order);
            return Ok(result);
        }
    }
}
