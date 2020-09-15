using AutoMapper;
using PDCore.Interfaces;
using PDCoreNew.Context.IContext;
using PDCoreNew.Repositories.Repo;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.BLL.Entities;
using TaskManager.DAL.Contracts;

namespace TaskManager.DAL.Repositories
{
    public class TicketRepository : SqlRepositoryEntityFrameworkDisconnected<Ticket>, ITicketRepository
    {
        public TicketRepository(IEntityFrameworkDbContext ctx, ILogger logger, IMapper mapper) : base(ctx, logger, mapper)
        {
        }

        public IQueryable<Ticket> Find(bool includeSubobjects)
        {
            if (!includeSubobjects)
            {
                return FindAll();
            }

            return FindAll()
                    .Include(t => t.Type)
                    .Include(t => t.Priority)
                    .Include(t => t.Status)
                    .Include(t => t.Contrahent)
                    .Include(t => t.Representative)
                    .Include(t => t.Operator);
        }

        public Task<Ticket> FindByIdAsync(int id, bool includeSubobjects)
        {
            return Find(includeSubobjects).SingleOrDefaultAsync(t => t.Id == id);
        }

        public Task<Ticket> FindByNumberAsync(string number, bool includeSubobjects)
        {
            return Find(includeSubobjects).SingleOrDefaultAsync(t => t.Number == number);
        }
    }
}
