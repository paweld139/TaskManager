using PDCore.Repositories.IRepo;
using PDCoreNew.Context.IContext;
using PDCoreNew.Factories.Fac.Repository;
using PDCoreNew.UnitOfWork;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Repositories;

namespace TaskManager.DAL
{
    public class TaskManagerUow : UnitOfWork, ITaskManagerUow
    {
        public TaskManagerUow(IRepositoryProvider repositoryProvider, IEntityFrameworkDbContext dbContext) : base(repositoryProvider, dbContext)
        {
        }

        public IDictionaryRepository Dictionaries { get { return GetRepo<DictionaryRepository>(); } }
        public ITicketRepository Tickets { get { return GetRepo<TicketRepository>(); } }
        public ISqlRepositoryEntityFrameworkDisconnected<Comment> Comments { get { return GetStandardRepoDisconnected<Comment>(); } }
        public IContrahentRepository Contrahents { get { return GetRepo<ContrahentRepository>(); } }
        public IEmployeeRepository Employees { get { return GetRepo<EmployeeRepository>(); } }
    }
}
