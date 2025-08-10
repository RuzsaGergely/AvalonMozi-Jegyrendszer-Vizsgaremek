using AvalonMozi.Application.Orders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvalonMozi.Application.Orders.Services
{
    public interface IOrderService
    {
        Task<string> ProcessOrderRequest(OrderRequestDto orderDto);
    }
}
