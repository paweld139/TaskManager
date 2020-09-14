using PDCore.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.BLL.Models;

namespace TaskManager.DAL.Contracts
{
    public interface IDictionaryRepository : ISqlRepositoryEntityFrameworkDisconnected<Dictionary>
    {
        Task<List<DictionaryBrief>> GetDictionaryBriefsAsync(string name, string value = null);
    }
}