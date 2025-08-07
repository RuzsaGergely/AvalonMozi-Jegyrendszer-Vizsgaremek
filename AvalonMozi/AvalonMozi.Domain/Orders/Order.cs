using AvalonMozi.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public Guid TechnicalId { get; set; }
        public User User { get; set; }
        public BillingInformation BillingInfo { get; set; }
        public List<OrderItem> Items { get; set; }
        public int PriceSumGross { get; set; }
        public int PriceSumNet { get; set; }
        public bool Deleted { get; set; }
    }
}
