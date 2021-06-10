using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETickets.Web.Models.Domain
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string MovieName { get; set; }
        public string MovieImage { get; set; }
        public string MovieDescription { get; set; }
        [Required]
        public int TicketPrice { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }

    }
}
