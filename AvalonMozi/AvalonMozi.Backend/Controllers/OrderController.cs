using AvalonMozi.Application.Orders.Dto;
using AvalonMozi.Application.Orders.Services;
using AvalonMozi.Factories.OrderFactories;
using AvalonMozi.Factories.OrderFactories.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvalonMozi.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderFactory _orderFactory;
        public OrderController(IOrderService orderService, IOrderFactory orderFactory)
        {
            _orderService = orderService;
            _orderFactory = orderFactory;
        }

        [HttpPost("NewOrder")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> AddNewOrder(OrderRequestDto order)
        {
            string userTechnicalId = this.User.Claims.First(i => i.Type == "UserTechnicalId").Value;
            order.UserTechnicalId = userTechnicalId;
            var result = await _orderService.ProcessOrderRequest(order);
            return Ok(result);
        }

        [HttpGet("GetUserBillingInformations")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<List<BillingInformationDto>> GetUserBillingInformations()
        {
            string userTechnicalId = this.User.Claims.First(i => i.Type == "UserTechnicalId").Value;
            return _orderFactory.ConvertBillingInfoListEntityToDtoList(await _orderService.GetBillingInformations(userTechnicalId));
        }
    }
}
