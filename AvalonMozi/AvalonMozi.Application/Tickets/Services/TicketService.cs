using AvalonMozi.Application.Tickets.Dto;
using AvalonMozi.Domain.Orders;
using AvalonMozi.Domain.Tickets;
using AvalonMozi.Domain.Users;
using AvalonMozi.Persistence;
using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

        public async Task<List<UserTicketDto>> GetUserTickets(string userId)
        {
            var returnList = new List<UserTicketDto>();

            var orders = await _context.Orders.Include(x=>x.Items).ThenInclude(x=> x.SelectedDateTime).Include(x => x.Items).ThenInclude(x => x.Movie).Where(x=>x.User.TechnicalId == userId).ToListAsync();
            foreach (var order in orders)
            {
                foreach (var orderItem in order.Items)
                {
                    var ticket = await _context.Tickets.Where(x=>x.AssignedTo.TechnicalId == orderItem.TechnicalId).FirstOrDefaultAsync();
                    var newTicketResponse = new UserTicketDto()
                    {
                        TicketData = ticket.TicketData,
                        MovieDate = $"{orderItem.SelectedDateTime.DateFrom.ToString()} - {orderItem.SelectedDateTime.DateTo.ToString()}",
                        MovieName = orderItem.Movie.Title
                    };

                    if (DateTime.Now < ticket.AssignedTo.SelectedDateTime.DateTo && DateTime.Now > ticket.AssignedTo.SelectedDateTime.DateFrom.AddMinutes(-30))
                    {
                        newTicketResponse.Valid = true;
                    }
                    else
                    {
                        newTicketResponse.Valid = false;
                    }
                    returnList.Add(newTicketResponse);
                }
            }
            
            return returnList;
        }
    }
}
