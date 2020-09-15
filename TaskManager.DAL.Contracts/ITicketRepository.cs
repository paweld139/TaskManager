using PDCore.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Entities;

namespace TaskManager.DAL.Contracts
{
    public interface ITicketRepository : ISqlRepositoryEntityFrameworkDisconnected<Ticket>
    {
        IQueryable<Ticket> Find(bool includeSubobjects);

        Task<Ticket> FindByIdAsync(int id, bool includeSubobjects);

        Task<Ticket> FindByNumberAsync(string number, bool includeSubobjects);
    }
}
