using PDCore.Repositories.IRepo;
using PDCoreNew.Context.IContext;
using PDCoreNew.Factories.Fac.Repository;
using PDCoreNew.Repositories.IRepo;
using PDCoreNew.UnitOfWork;
using PDWebCore.Models;
using PDWebCore.Repositories.Repo;
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
        public ICommentRepository Comments { get { return GetRepo<CommentRepository>(); } }
        public IContrahentRepository Contrahents { get { return GetRepo<ContrahentRepository>(); } }
        public IEmployeeRepository Employees { get { return GetRepo<EmployeeRepository>(); } }
        public ISqlRepositoryEntityFrameworkDisconnected<UserDataModel> UserData { get { return GetStandardRepoDisconnected<UserDataModel>(); } }
        public IFileRepository FilesBase { get { return GetRepo<FileRepository>(); } }

        public ISqlRepositoryEntityFrameworkDisconnected<File> Files { get { return GetStandardRepoDisconnected<File>(); } }
    }
}
