using AutoMapper;
using PDCore.Extensions;
using PDCore.Interfaces;
using PDCoreNew.Context.IContext;
using PDCoreNew.Repositories.Repo;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Repositories
{
    public class ContrahentRepository : SqlRepositoryEntityFrameworkDisconnected<Contrahent>, IContrahentRepository
    {
        public ContrahentRepository(IEntityFrameworkDbContext ctx, ILogger logger, IMapper mapper) : base(ctx, logger, mapper)
        {
        }

        public IQueryable<ContrahentBrief> FindBriefs(bool orderByName = true)
        {
            var query = FindAll<ContrahentBrief>();

            if (orderByName)
            {
                query = query.OrderBy(c => c.Name);
            }

            return query;
        }

        public async Task<List<KeyValuePair<int, string>>> GetKeyValuePairsAsync(bool orderByName = true)
        {
            var data = await FindBriefs().ToListAsync().ConfigureAwait(false);

            return data.GetKVP(c => c.Id, c => c.Name);
        }
    }
}
