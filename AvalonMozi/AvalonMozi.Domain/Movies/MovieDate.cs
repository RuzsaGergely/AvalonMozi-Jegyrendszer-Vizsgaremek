using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Movies
{
    public class MovieDate
    {
        public int Id { get; set; }
        public Guid TechnicalId {  get; set; }
        public DateTime Date { get; set; }
        public bool Deleted { get; set; }
    }
}
