using AutoMapper;
using PDCore.Interfaces;
using PDCoreNew.Context.IContext;
using PDCoreNew.Repositories.Repo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskManager.BLL.Entities;
using TaskManager.BLL.Entities.Details;
using TaskManager.DAL.Contracts;

namespace TaskManager.DAL.Repositories
{
    public class TicketRepository : SqlRepositoryEntityFrameworkDisconnected<Ticket>, ITicketRepository
    {
        public TicketRepository(IEntityFrameworkDbContext ctx, ILogger logger, IMapper mapper) : base(ctx, logger, mapper)
        {
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
                    .Include(t => t.Operator);
        }

        public Task<Ticket> FindByIdAsync(int id, bool includeSubobjects, bool asNoTracking = true)
        {
            return Find(includeSubobjects, asNoTracking).SingleOrDefaultAsync(t => t.Id == id);
        }

        public Task<Ticket> FindByNumberAsync(string number, bool includeSubobjects)
        {
            return Find(includeSubobjects).SingleOrDefaultAsync(t => t.Number == number);
        }

        public Task<TicketDetails> FindDetailsByIdAsync(int id)
        {
            return FindAll<TicketDetails>().SingleOrDefaultAsync(t => t.Id == id);
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

        private ICollection<string> GetPropertiesForUpdate(IPrincipal principal)
        {
            Ticket ticket;

            var properties = new List<string>
            {
                nameof(ticket.PriorityId),
                nameof(ticket.Budget),
                nameof(ticket.OperatorId)
            };

            if (principal.IsInRole("Klient"))
            {
                properties.RemoveRange(1, 2);
            }
            else
            {
                properties.RemoveAt(0);
            }

            return properties;
        }

        public void Update(Ticket ticket, IPrincipal principal)
        {
            if (principal.IsInRole("Admin"))
            {
                Update(ticket);

                return;
            }

            var properties = GetPropertiesForUpdate(principal);

            UpdateWithIncludeOrExcludeProperties(ticket, true, properties);
        }

        public void Update(TicketDetails source, Ticket destination, IPrincipal principal)
        {
            if (principal.IsInRole("Admin"))
            {
                mapper.Map(source, destination);

                return;
            }

            var properties = GetPropertiesForUpdate(principal);

            UpdateWithIncludeOrExcludeProperties(source, destination, true, properties);
        }

        public Task<bool> SaveUpdatedWithOptimisticConcurrencyAsync(Ticket ticket, IPrincipal principal, Action<string, string> writeError)
        {
            Update(ticket, principal);

            return SaveUpdatedWithOptimisticConcurrencyAsync(ticket, writeError, false);
        }

        public async Task<bool> SaveUpdatedWithOptimisticConcurrencyAsync(TicketDetails source, Ticket destination, IPrincipal principal, Action<string, string> writeError)
        {
            Update(source, destination, principal);

            var result = await SaveUpdatedWithOptimisticConcurrencyAsync(destination, writeError, false);

            mapper.Map(destination, source);

            return result;
        }
    }
}
