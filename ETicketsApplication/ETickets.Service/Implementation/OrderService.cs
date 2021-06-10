using ETickets.Domain.DomainModels;
using ETickets.Domain.DTO;
using ETickets.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETickets.Repository.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        public OrderService(IUserRepository userRepository, ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<OrderDto> GetAllOrdersAsync(string id)
        {
            var user = this._userRepository.Get(id);

            var userOrders = user.Orders;

            List<TicketInOrder> ticketsInOrder = new List<TicketInOrder>();
            List<Order> orders = new List<Order>();

            foreach (var item in userOrders)
            {
                var order = await this._context.Orders.Where(z => z.Id.Equals(item.Id)).Include(z => z.TicketsInOrder).FirstOrDefaultAsync();
                orders.Add(order);
                var tickets = _context.TicketInOrder.Where(z => z.OrderId.Equals(order.Id)).Include(z => z.OrderedTicket);
                foreach (var ticket in tickets)
                {
                    ticketsInOrder.Add(ticket);
                }
            }

            OrderDto orderDto = new OrderDto
            {
                Orders = orders,
                TicketsInOrder = ticketsInOrder
            };

            return orderDto;
        }
    }
}
