using AvalonMozi.Domain.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Factories.MovieFactories.Dto
{
    public class MovieDto
    {
        public string? TechnicalId { get; set; }
        public string Title { get; set; }
        public string SeoFriendlyTitle { get; set; }
        public string Description { get; set; }
        public string AgeRestriction { get; set; }
        public int TicketPrice { get; set; }
        public List<MovieDateDto> Dates { get; set; }
    }
}
