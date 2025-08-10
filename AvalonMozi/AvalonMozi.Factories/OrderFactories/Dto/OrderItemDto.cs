using AvalonMozi.Domain.Movies;
using AvalonMozi.Factories.MovieFactories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Factories.OrderFactories.Dto
{
    public class OrderItemDto
    {
        public string TechnicalId { get; set; }
        public MovieDto Movie { get; set; }
        public MovieDateDto SelectedDateTime { get; set; }
    }
}
