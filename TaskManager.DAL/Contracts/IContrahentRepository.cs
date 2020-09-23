using PDCore.Repositories.IRepo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Contracts
{
    public interface IContrahentRepository : ISqlRepositoryEntityFrameworkDisconnected<Contrahent>
    {
        IQueryable<ContrahentBrief> FindBriefs(bool orderByName = true);

        Task<List<KeyValuePair<int, string>>> GetKeyValuePairsAsync(bool orderByName = true);
    }
}