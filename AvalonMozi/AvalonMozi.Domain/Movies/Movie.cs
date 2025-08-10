using AvalonMozi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Movies
{
    public class Movie : BaseEntity
    {
        public string TechnicalId { get; set; }
        public string Title { get; set; }
        public string SeoFriendlyTitle { get; set; }
        public string Description { get; set; }
        public string AgeRestriction { get; set; }
        public int TicketPrice { get; set; }
        public List<MovieDate> Dates { get; set; }
    }
}
