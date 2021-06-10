using ETickets.Domain.DomainModels;
using ETickets.Domain.DTO;
using ETickets.Repository.Interface;
using ETickets.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETickets.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepositorty;
        private readonly IRepository<Order> _orderRepositorty;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, 
            IRepository<TicketInOrder> ticketInOrderRepository, 
            IRepository<Order> orderRepositorty, 
            IUserRepository userRepository)
        {
            _shoppingCartRepositorty = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepositorty = orderRepositorty;
            _ticketInOrderRepository = ticketInOrderRepository;
        }

        public bool deleteTicketFromShoppingCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.TicketsInShoppingCart.Where(z => z.TicketId.Equals(id)).FirstOrDefault();

                userShoppingCart.TicketsInShoppingCart.Remove(itemToDelete);

                this._shoppingCartRepositorty.Update(userShoppingCart);

                return true;
            }

            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userShoppingCart = loggedInUser.UserCart;

            var AllProducts = userShoppingCart.TicketsInShoppingCart.ToList();

            var allProductPrice = AllProducts.Select(z => new
            {
                ProductPrice = z.Ticket.TicketPrice,
                Quanitity = z.Quantity
            }).ToList();

            var totalPrice = 0;


            foreach (var item in allProductPrice)
            {
                totalPrice += item.Quanitity * item.ProductPrice;
            }


            ShoppingCartDto scDto = new ShoppingCartDto
            {
                Tickets = AllProducts,
                TotalPrice = totalPrice
            };


            return scDto;

        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepositorty.Insert(order);

                List<TicketInOrder> ticketsInOrder = new List<TicketInOrder>();

                var result = userShoppingCart.TicketsInShoppingCart.Select(z => new TicketInOrder
                {
                    Id = Guid.NewGuid(),
                    TicketId = z.Ticket.Id,
                    OrderedTicket = z.Ticket,
                    OrderId = order.Id,
                    UserOrder = order,
                    Quantity = z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0;

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i-1];

                    totalPrice += item.Quantity * item.OrderedTicket.TicketPrice;
                }

                ticketsInOrder.AddRange(result);

                foreach (var item in ticketsInOrder)
                {
                    this._ticketInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.TicketsInShoppingCart.Clear();

                this._userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
    }
}
