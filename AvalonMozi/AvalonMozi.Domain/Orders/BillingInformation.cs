using AvalonMozi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Domain.Orders
{
    public class BillingInformation : BaseEntity
    {
        public string Name { get; set; }
        public string? CompanyName { get; set; }
        public string? VATNumber { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string ZipCode {  get; set; }
        public string City { get; set; }
        public string County { get; set; }
    }
}
