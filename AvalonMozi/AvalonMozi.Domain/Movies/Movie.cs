using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Movies
{
    public class Movie
    {
        public int Id { get; set; }
        public Guid TechnicalId { get; set; }
        public string Title { get; set; }
        public string SeoFriendlyTitle { get; set; }
        public string Description { get; set; }
        public string AgeRestriction { get; set; }
        public int TicketPrice { get; set; }
        public List<MovieDate> Dates { get; set; }
        public bool Deleted { get; set; }
    }
}
