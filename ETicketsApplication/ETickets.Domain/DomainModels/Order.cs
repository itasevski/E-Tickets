using ETickets.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETickets.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public ETicketsApplicationUser User { get; set; }

        public ICollection<TicketInOrder> TicketsInOrder { get; set; }
    }
}
