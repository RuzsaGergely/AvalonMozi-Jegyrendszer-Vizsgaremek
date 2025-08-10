using AvalonMozi.Application.Tickets.Dto;
using AvalonMozi.Domain.Orders;
using AvalonMozi.Domain.Tickets;
using AvalonMozi.Domain.Users;
using AvalonMozi.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Tickets.Services
{
    public class TicketService : ITicketService
    {
        private readonly AvalonContext _context;
        public TicketService(AvalonContext context)
        {
            _context = context;
        }

        public async Task<TicketCheckResponseDto> CheckTicket(string ticketData)
        {
            var response = new TicketCheckResponseDto();

            var base64EncodedBytes = Convert.FromBase64String(ticketData);
            string decodedData = Encoding.UTF8.GetString(base64EncodedBytes);
            string[] dataparts = decodedData.Split(':');

            var ticket = await _context.Tickets.FirstOrDefaultAsync(x=>x.TicketData == ticketData);
            if (ticket is not null)
            {
                response.MovieName = ticket.AssignedTo.Movie.Title;
                response.MovieDate = $"{ticket.AssignedTo.SelectedDateTime.DateFrom} - {ticket.AssignedTo.SelectedDateTime.DateTo}";
                if(DateTime.Now < ticket.AssignedTo.SelectedDateTime.DateTo && DateTime.Now > ticket.AssignedTo.SelectedDateTime.DateFrom.AddMinutes(-30))
                {
                    response.Valid = true;
                    response.Message = "A jegy érvényes!";
                } else
                {
                    response.Valid = false;
                    response.Message = "A jegy érvénytelen!";
                }
            }
            return response;
        }

        public Ticket GenerateTicket(OrderItem item, User user)
        {
            string ticketContent = $"{item.TechnicalId}:{user.TechnicalId}:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
            return new Ticket()
            {
                AssignedTo = item,
                Deleted = false,
                TicketData = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ticketContent))
            };
        }
    }
}
