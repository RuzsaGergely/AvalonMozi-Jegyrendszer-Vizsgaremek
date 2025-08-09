using AvalonMozi.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Factories.UserFactories.Dto
{
    public class RoleDto
    {
        public string TechnicalName { get; set; }
        public string DisplayName { get; set; }
        public bool Deleted { get; set; }
    }
}
