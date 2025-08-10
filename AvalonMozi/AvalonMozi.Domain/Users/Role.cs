using AvalonMozi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Users
{
    public class Role : BaseEntity
    {
        public string TechnicalName { get; set; }
        public string DisplayName { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
