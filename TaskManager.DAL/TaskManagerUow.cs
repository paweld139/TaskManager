using PDCore.Repositories.IRepo;
using PDCoreNew.Context.IContext;
using PDCoreNew.Factories.Fac.Repository;
using PDCoreNew.UnitOfWork;
using TaskManager.BLL.Models;
using TaskManager.DAL.Contracts;

namespace TaskManager.DAL
{
    public class TaskManagerUow : UnitOfWork, ITaskManagerUow
    {
        public TaskManagerUow(IRepositoryProvider repositoryProvider, IEntityFrameworkDbContext dbContext) : base(repositoryProvider, dbContext)
        {
        }

        public ISqlRepositoryEntityFrameworkDisconnected<Dictionary> Dicionaries { get { return GetStandardRepoDisconnected<Dictionary>(); } }
        public ISqlRepositoryEntityFrameworkDisconnected<Ticket> Tickets { get { return GetStandardRepoDisconnected<Ticket>(); } }
    }
}
