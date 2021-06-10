using ETickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETickets.Domain.DTO
{
    public class OrderDto
    {
        public List<Order> Orders { get; set; }
        public List<TicketInOrder> TicketsInOrder { get; set; }
    }
}
