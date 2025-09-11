using AvalonMozi.Application.Tickets.Dto;
using AvalonMozi.Application.Tickets.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvalonMozi.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpGet("CheckTicket")]
        [Authorize(Roles = "ADMIN,EMPLOYEE")]
        public async Task<TicketCheckResponseDto> CheckTicketValidity(string ticketData)
        {
            return await _ticketService.CheckTicket(ticketData);
        }

        [HttpGet("GetUserTickets")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<List<UserTicketDto>> GetUserTickets()
        {
            string userTechnicalId = this.User.Claims.First(i => i.Type == "UserTechnicalId").Value;

            return await _ticketService.GetUserTickets(userTechnicalId);
        }
    }
}
