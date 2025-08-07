using AvalonMozi.Domain.Movies;
using AvalonMozi.Domain.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Orders
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Guid TechnicalId {  get; set; }
        public Movie Movie { get; set; }
        public bool Deleted { get; set; }
    }
}
