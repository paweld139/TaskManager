using AutoMapper;
using PDCore.Interfaces;
using PDCoreNew.Context.IContext;
using PDCoreNew.Repositories.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DAL.Contracts;
using System.Data.Entity;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Repositories
{
    public class DictionaryRepository : SqlRepositoryEntityFrameworkDisconnected<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(IEntityFrameworkDbContext ctx, ILogger logger, IMapper mapper) : base(ctx, logger, mapper)
        {
        }

        private IQueryable<Dictionary> Find(string name, string value = null)
        {
            return Find(d => d.Name == name && (value == null || d.Value == value));
        }

        public Task<List<Dictionary>> GetAsync(string name, string value = null)
        {
            return Find(name, value).ToListAsync();
        }

        public IQueryable<DictionaryBrief> FindBriefs(string name, string value = null, bool orderByValue = true)
        {
            var query = mapper.ProjectTo<DictionaryBrief>(Find(name, value));

            if(orderByValue)
            {
                query = query.OrderBy(d => d.Value);
            }

            return query;
        }

        public Task<List<DictionaryBrief>> GetBriefsAsync(string name, string value = null)
        {
            return FindBriefs(name, value, false).ToListAsync();
        }
    }
}
