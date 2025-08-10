using AvalonMozi.Domain.Common;
using AvalonMozi.Domain.Movies;
using AvalonMozi.Domain.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Orders
{
    public class OrderItem : BaseEntity
    {
        public string TechnicalId {  get; set; }
        public Movie Movie { get; set; }
        public MovieDate SelectedDateTime { get; set; }
    }
}
