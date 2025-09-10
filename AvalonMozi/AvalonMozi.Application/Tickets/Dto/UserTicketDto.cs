using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Tickets.Dto
{
    public class UserTicketDto
    {
        public string TicketData { get; set; }
        public string MovieName { get; set; }
        public string MovieDate { get; set; }
        public bool Valid { get; set; }
    }
}
