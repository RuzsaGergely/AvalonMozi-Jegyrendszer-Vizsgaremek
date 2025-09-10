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

        public async Task<List<BillingInformation>> GetBillingInformations(string usertechid)
        {
            var user = await _context.Users.Include(x=>x.BillingInformations).Where(x=>x.TechnicalId == usertechid).FirstOrDefaultAsync();
            return user.BillingInformations;
        }

        public async Task<string> ProcessOrderRequest(OrderRequestDto orderDto)
        {
            // Create entity with billing data, technical ID and User attachment
            var newOrder = new Order()
            {
                User = await _context.Users.FirstOrDefaultAsync(x => x.TechnicalId == orderDto.UserTechnicalId),
                TechnicalId = Guid.NewGuid().ToString(),
                Items = new List<OrderItem>()
            };

            if(orderDto.BillingInfo.TechnicalId == "NEWBILLINGINFO")
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.TechnicalId == orderDto.UserTechnicalId);
                var newBillingInfo = new BillingInformation()
                {
                    Address1 = orderDto.BillingInfo.Address1,
                    Address2 = orderDto.BillingInfo.Address2,
                    City = orderDto.BillingInfo.City,
                    CompanyName = orderDto.BillingInfo.CompanyName,
                    County = orderDto.BillingInfo.County,
                    Name = orderDto.BillingInfo.Name,
                    VATNumber = orderDto.BillingInfo.VATNumber,
                    ZipCode = orderDto.BillingInfo.ZipCode,
                    Deleted = false,
                    TechnicalId = Guid.NewGuid().ToString()
                };
                user.BillingInformations.Add(newBillingInfo);
                await _context.SaveChangesAsync();
                newOrder.BillingInfo = newBillingInfo;
            } else
            {
                var billingData = await _context.BillingInformations.Where(x=>x.TechnicalId == orderDto.BillingInfo.TechnicalId).FirstOrDefaultAsync();
                newOrder.BillingInfo = billingData;
            }

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
