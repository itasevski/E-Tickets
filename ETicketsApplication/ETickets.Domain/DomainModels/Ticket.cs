using ETickets.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETickets.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Required]
        public string MovieName { get; set; }
        public string MovieImage { get; set; }
        public string MovieDescription { get; set; }
        public Genre MovieGenre { get; set; }
        public DateTime StartDate { get; set; }
        [Required]
        public int TicketPrice { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public ICollection<TicketInOrder> TicketInOrders { get; set; }

    }
}
