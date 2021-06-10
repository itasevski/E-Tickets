using ETickets.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETickets.Web.Models.Domain
{
    public class ShoppingCart
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public ETicketsApplicationUser Owner { get; set; }
        [Required]
        public virtual ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}
