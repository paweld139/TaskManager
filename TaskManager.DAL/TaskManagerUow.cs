using PDCore.Repositories.IRepo;
using PDCoreNew.Context.IContext;
using PDCoreNew.Factories.Fac.Repository;
using PDCoreNew.UnitOfWork;
using TaskManager.BLL.Models;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Repositories;

namespace TaskManager.DAL
{
    public class TaskManagerUow : UnitOfWork, ITaskManagerUow
    {
        public TaskManagerUow(IRepositoryProvider repositoryProvider, IEntityFrameworkDbContext dbContext) : base(repositoryProvider, dbContext)
        {
        }

        public IDictionaryRepository Dictionaries { get { return GetRepo<DictionaryRepository>(); } }
        public ISqlRepositoryEntityFrameworkDisconnected<Ticket> Tickets { get { return GetStandardRepoDisconnected<Ticket>(); } }
    }
}
