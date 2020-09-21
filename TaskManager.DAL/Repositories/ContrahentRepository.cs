using AutoMapper;
using PDCore.Interfaces;
using PDCoreNew.Context.IContext;
using PDCoreNew.Repositories.Repo;
using System.Linq;
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
            var query = FindAll();

            if (orderByName)
            {
                query = query.OrderBy(c => c.Name);
            }

            return query;
        }
    }
}
