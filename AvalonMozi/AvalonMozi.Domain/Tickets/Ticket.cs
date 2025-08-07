using AvalonMozi.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Tickets
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime ValidUntil {  get; set; }
        public string TicketData { get; set; }
        public OrderItem AssignedTo { get; set; }
        public bool Deleted { get; set; }
    }
}
