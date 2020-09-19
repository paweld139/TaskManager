using AutoMapper;
using PDCore.Interfaces;
using PDCoreNew.Context.IContext;
using PDCoreNew.Repositories.Repo;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Details;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Repositories
{
    public class TicketRepository : SqlRepositoryEntityFrameworkDisconnected<Ticket>, ITicketRepository
    {
        private readonly IDataAccessStrategy<Ticket> dataAccessStrategy;

        public TicketRepository(IEntityFrameworkDbContext ctx,
            ILogger logger,
            IMapper mapper,
            IDataAccessStrategy<Ticket> dataAccessStrategy) : base(ctx, logger, mapper)
        {
            this.dataAccessStrategy = dataAccessStrategy;
        }

        public override IQueryable<Ticket> FindAll(bool asNoTracking)
        {
            return dataAccessStrategy.PrepareQuery(base.FindAll(asNoTracking));
        }

        public IQueryable<Ticket> Find(bool includeSubobjects, bool asNoTracking = true)
        {
            if (!includeSubobjects)
            {
                return FindAll(asNoTracking);
            }

            return FindAll(asNoTracking)
                    .Include(t => t.Type)
                    .Include(t => t.Priority)
                    .Include(t => t.Status)
                    .Include(t => t.Contrahent)
                    .Include(t => t.Representative)
                    .Include(t => t.Operator)
                    .Include(t => t.Comments);
        }

        public Task<Ticket> FindByIdAsync(int id, bool includeSubobjects, bool asNoTracking = true)
        {
            return Find(includeSubobjects, asNoTracking).SingleOrDefaultAsync(t => t.Id == id);
        }

        public Task<Ticket> FindByNumberAsync(string number, bool includeSubobjects)
        {
            return Find(includeSubobjects).SingleOrDefaultAsync(t => t.Number == number);
        }

        public Task<TicketDetails> FindDetailsByNumberAsync(string number)
        {
            return FindAll<TicketDetails>().SingleOrDefaultAsync(t => t.Number == number);
        }

        public override void Update(Ticket entity)
        {
            base.Update(entity);

            ctx.Entry(entity).Property(e => e.Number).IsModified = false;
        }
    }
}
