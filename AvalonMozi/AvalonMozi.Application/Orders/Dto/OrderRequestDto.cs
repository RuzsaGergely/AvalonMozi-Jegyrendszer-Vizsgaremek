using AvalonMozi.Domain.Users;
using AvalonMozi.Factories.OrderFactories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Orders.Dto
{
    public class OrderRequestDto
    {
        public string UserTechnicalId { get; set; }
        public BillingInformationDto BillingInfo { get; set; }
        public List<OrderItemRequestDto> Items { get; set; }
    }
}
