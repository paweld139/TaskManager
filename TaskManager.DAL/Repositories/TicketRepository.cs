using AutoMapper;
using PDCore.Interfaces;
using PDCore.Models;
using PDCoreNew.Context.IContext;
using PDCoreNew.Repositories.Repo;
using System;
using System.Collections.Generic;
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
        public TicketRepository(IEntityFrameworkDbContext ctx,
            ILogger logger,
            IMapper mapper) : base(ctx, logger, mapper)
        {
        }

        public TicketRepository(IEntityFrameworkDbContext ctx,
             ILogger logger,
             IMapper mapper,
             IDataAccessStrategy<Ticket> strategy) : base(ctx, logger, mapper, strategy)
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

        /// <summary>
        /// Get the unique tags from all of the tickets
        /// as a list of <see cref="TagGroup"/>.
        /// </summary>
        /// <remarks>
        ///See <see cref="ITicketRepository.GetTagGroups"/> for details.
        /// </remarks>
        public IEnumerable<TagGroup> GetTagGroups()
        {

            var tagGroups =
                // extract the delimited tags string and ticket id from all tickets
                FindAll().Select(s => new { s.Tags, s.Id })
                    .ToArray() // we'll process them in memory.

                    // split the "Tags" string into individual tags 
                    // and flatten into {tag, id} pairs
                    .SelectMany(
                        s =>
                        s.Tags.Split(_tagDelimiter, StringSplitOptions.RemoveEmptyEntries)
                            .Select(t => new { Tag = t, s.Id })
                    )

                    // group {tag, id} by tag into unique {tag, [session-id-array]}
                    .GroupBy(g => g.Tag, data => data.Id)

                    // project the group into TagGroup instances
                    // ensuring that ids array in each array are unique
                    .Select(tg => new TagGroup
                    {
                        Tag = tg.Key,
                        Ids = tg.Distinct().ToArray(),
                    })
                    .OrderBy(tg => tg.Tag);

            return tagGroups;
        }

        private readonly char[] _tagDelimiter = new[] { '|' };


        //public override void Update(Ticket entity)
        //{
        //    base.Update(entity);

        //    ctx.Entry(entity).Property(e => e.Number).IsModified = false;
        //}

        //public override IQueryable<Ticket> FindAll(bool asNoTracking)
        //{
        //    return dataAccessStrategy.PrepareQuery(base.FindAll(asNoTracking));
        //}
    }
}
