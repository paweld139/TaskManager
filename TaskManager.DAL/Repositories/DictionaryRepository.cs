using AutoMapper;
using PDCore.Interfaces;
using PDCoreNew.Context.IContext;
using PDCoreNew.Repositories.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.BLL.Models;
using TaskManager.DAL.Contracts;
using System.Data.Entity;
using PDCore.Extensions;
using System.Linq.Expressions;
using System;

namespace TaskManager.DAL.Repositories
{
    public class DictionaryRepository : SqlRepositoryEntityFrameworkDisconnected<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(IEntityFrameworkDbContext ctx, ILogger logger, IMapper mapper) : base(ctx, logger, mapper)
        {
        }

        public Task<List<DictionaryBrief>> GetDictionaryBriefsAsync(string name, string value = null)
        {
            return GetAsync<DictionaryBrief>(d => d.Name == name && (value == null || d.Value == value));
        }
    }
}
