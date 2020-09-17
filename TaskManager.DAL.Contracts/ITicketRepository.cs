using PDCore.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Entities;
using TaskManager.BLL.Entities.Details;
using TaskManager.BLL.Models;

namespace TaskManager.DAL.Contracts
{
    public interface ITicketRepository : ISqlRepositoryEntityFrameworkDisconnected<Ticket>
    {
        IQueryable<Ticket> Find(bool includeSubobjects, bool asNoTracking = true);

        Task<Ticket> FindByIdAsync(int id, bool includeSubobjects, bool asNoTracking = true);

        Task<Ticket> FindByNumberAsync(string number, bool includeSubobjects);

        void Update(Ticket ticket, IPrincipal principal);

        void Update(TicketModel source, Ticket destination, IPrincipal principal);

        Task<bool> SaveUpdatedWithOptimisticConcurrencyAsync(Ticket ticket, IPrincipal principal, Action<string, string> writeError);

        Task<bool> SaveUpdatedWithOptimisticConcurrencyAsync(TicketModel source, Ticket destination, IPrincipal principal, Action<string, string> writeError);
        
        Task<TicketDetails> FindDetailsByIdAsync(int id);

        Task<TicketDetails> FindDetailsByNumberAsync(string number);
    }
}
