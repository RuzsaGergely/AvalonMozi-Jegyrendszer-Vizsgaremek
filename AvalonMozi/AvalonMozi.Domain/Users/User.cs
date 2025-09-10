using AvalonMozi.Domain.Common;
using AvalonMozi.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Users
{
    public class User : BaseEntity
    {
        public string TechnicalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string PasswordHash { get; set; }
        public DateTime LastSuccessfulLoginTime { get; set; } = new DateTime(1970, 1, 1);
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<BillingInformation> BillingInformations { get; set; } = new List<BillingInformation>();
    }
}
