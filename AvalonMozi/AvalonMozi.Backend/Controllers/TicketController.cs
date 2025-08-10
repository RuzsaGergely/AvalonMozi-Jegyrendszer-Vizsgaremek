﻿using AvalonMozi.Application.Tickets.Dto;
using AvalonMozi.Application.Tickets.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvalonMozi.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN,EMPLOYEE")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        [HttpGet("CheckTicket")]
        public async Task<TicketCheckResponseDto> CheckTicketValidity(string ticketData)
        {
            return await _ticketService.CheckTicket(ticketData);
        }
    }
}
