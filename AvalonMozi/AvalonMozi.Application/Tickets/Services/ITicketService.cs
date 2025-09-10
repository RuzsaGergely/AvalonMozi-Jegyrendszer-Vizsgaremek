using AvalonMozi.Application.Tickets.Dto;
using AvalonMozi.Domain.Orders;
using AvalonMozi.Domain.Tickets;
using AvalonMozi.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Tickets.Services
{
    public interface ITicketService
    {
        Task<TicketCheckResponseDto> CheckTicket(string ticketData);
        Ticket GenerateTicket(OrderItem item, User user);
        Task<List<UserTicketDto>> GetUserTickets(string userId);
    }
}
