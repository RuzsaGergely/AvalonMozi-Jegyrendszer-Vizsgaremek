using AvalonMozi.Domain.Orders;
using AvalonMozi.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Factories.OrderFactories.Dto
{
    public class OrderDto
    {
        public string TechnicalId { get; set; }
        public User User { get; set; }
        public BillingInformationDto BillingInfo { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public int PriceSumGross { get; set; }
        public int PriceSumNet { get; set; }
    }
}
