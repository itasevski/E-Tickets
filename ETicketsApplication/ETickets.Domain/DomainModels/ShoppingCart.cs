using ETickets.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETickets.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public ETicketsApplicationUser Owner { get; set; }
        [Required]
        public virtual ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}
