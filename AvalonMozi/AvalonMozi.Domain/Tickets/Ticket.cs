using AvalonMozi.Domain.Common;
using AvalonMozi.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Tickets
{
    public class Ticket : BaseEntity
    {
        public DateTime ValidUntil {  get; set; }
        public string TicketData { get; set; }
        public OrderItem AssignedTo { get; set; }
    }
}
