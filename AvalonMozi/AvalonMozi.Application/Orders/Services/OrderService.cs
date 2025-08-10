using AvalonMozi.Application.Orders.Dto;
using AvalonMozi.Application.Tickets.Services;
using AvalonMozi.Domain.Orders;
using AvalonMozi.Domain.Tickets;
using AvalonMozi.Factories.OrderFactories;
using AvalonMozi.Factories.OrderFactories.Dto;
using AvalonMozi.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly AvalonContext _context;
        private readonly IOrderFactory _orderFactory;
        private readonly ITicketService _ticketService;
        public OrderService(IOrderFactory orderFactory, AvalonContext context, ITicketService ticketService)
        {
            _orderFactory = orderFactory;
            _context = context;
            _ticketService = ticketService;
        }
        public async Task<string> ProcessOrderRequest(OrderRequestDto orderDto)
        {
            // Create entity with billing data, technical ID and User attachment
            var newOrder = new Order()
            {
                BillingInfo = new BillingInformation()
                {
                    Address1 = orderDto.BillingInfo.Address1,
                    Address2 = orderDto.BillingInfo.Address2,
                    City = orderDto.BillingInfo.City,
                    CompanyName = orderDto.BillingInfo.CompanyName,
                    County = orderDto.BillingInfo.County,
                    Name = orderDto.BillingInfo.Name,
                    VATNumber = orderDto.BillingInfo.VATNumber,
                    ZipCode = orderDto.BillingInfo.ZipCode,
                    Deleted = false
                },
                User = await _context.Users.FirstOrDefaultAsync(x => x.TechnicalId == orderDto.UserTechnicalId),
                TechnicalId = Guid.NewGuid().ToString(),
                Items = new List<OrderItem>()
            };

            // add ordered movies and dates to order items
            foreach (var item in orderDto.Items)
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(x => x.TechnicalId == item.MovieTechnicalId);
                var date = movie.Dates.FirstOrDefault(x => x.TechnicalId == item.DateTimeTechnicalId);

                newOrder.Items.Add(new OrderItem()
                {
                    Deleted = false,
                    SelectedDateTime = date,
                    Movie = movie,
                    TechnicalId = Guid.NewGuid().ToString()
                });
            }

            // calculate price and VAT (18%)
            foreach (var item in newOrder.Items)
            {
                newOrder.PriceSumNet += item.Movie.TicketPrice;
            }
            newOrder.PriceSumGross = Convert.ToInt32(Math.Round(newOrder.PriceSumNet * 1.18));

            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            // generating tickets

            foreach (var item in newOrder.Items)
            {
                _context.Tickets.Add(_ticketService.GenerateTicket(item, newOrder.User));
            }

            await _context.SaveChangesAsync();

            return newOrder.TechnicalId;
        }
    }
}
