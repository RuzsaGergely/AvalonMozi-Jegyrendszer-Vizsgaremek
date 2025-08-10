using AvalonMozi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Movies
{
    public class MovieDate : BaseEntity
    {
        public string TechnicalId {  get; set; }
        public DateTime Date { get; set; }
    }
}
