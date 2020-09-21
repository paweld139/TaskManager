using PDCore.Repositories.IRepo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Contracts
{
    public interface IDictionaryRepository : ISqlRepositoryEntityFrameworkDisconnected<Dictionary>
    {
        Task<List<Dictionary>> GetAsync(string name, string value = null);

        Task<List<DictionaryBrief>> GetBriefsAsync(string name, string value = null);

        IQueryable<DictionaryBrief> FindBriefs(string name, string value = null, bool orderByValue = true);
    }
}