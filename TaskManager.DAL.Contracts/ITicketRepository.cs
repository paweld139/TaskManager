using PDCore.Repositories.IRepo;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskManager.BLL.Entities;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.Details;

namespace TaskManager.DAL.Contracts
{
    public interface ITicketRepository : ISqlRepositoryEntityFrameworkDisconnected<Ticket>
    {
        IQueryable<Ticket> Find(bool includeSubobjects, bool asNoTracking = true);

        Task<Ticket> FindByIdAsync(int id, bool includeSubobjects, bool asNoTracking = true);

        Task<Ticket> FindByNumberAsync(string number, bool includeSubobjects);

        void Update(Ticket ticket, IPrincipal principal);

        void Update(TicketBasic source, Ticket destination, IPrincipal principal);

        Task<bool> SaveUpdatedWithOptimisticConcurrencyAsync(Ticket ticket, IPrincipal principal, Action<string, string> writeError);

        Task<bool> SaveUpdatedWithOptimisticConcurrencyAsync(TicketBasic source, Ticket destination, IPrincipal principal, Action<string, string> writeError);

        Task<TicketDetails> FindDetailsByNumberAsync(string number);
    }
}
