using ETickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETickets.Repository.Interface
{
    public interface IOrderService
    {

        Task<OrderDto> GetAllOrdersAsync(string id);

    }
}
