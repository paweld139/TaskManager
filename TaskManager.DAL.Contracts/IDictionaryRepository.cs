using PDCore.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.BLL.Entities;
using TaskManager.BLL.Entities.Briefs;

namespace TaskManager.DAL.Contracts
{
    public interface IDictionaryRepository : ISqlRepositoryEntityFrameworkDisconnected<Dictionary>
    {
        Task<List<Dictionary>> GetAsync(string name, string value = null);

        Task<List<DictionaryBrief>> GetBriefsAsync(string name, string value = null);
    }
}