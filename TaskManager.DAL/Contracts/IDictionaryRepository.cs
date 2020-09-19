using PDCore.Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Contracts
{
    public interface IDictionaryRepository : ISqlRepositoryEntityFrameworkDisconnected<Dictionary>
    {
        Task<List<Dictionary>> GetAsync(string name, string value = null);

        Task<List<DictionaryBrief>> GetBriefsAsync(string name, string value = null);
    }
}