using ETickets.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETickets.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<ETicketsApplicationUser> GetAll();
        ETicketsApplicationUser Get(string id);
        void Insert(ETicketsApplicationUser entity);
        void Update(ETicketsApplicationUser entity);
        void Delete(ETicketsApplicationUser entity);
    }
}
