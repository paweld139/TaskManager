using PDCore.Repositories.IRepo;
using PDWebCore.Models;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Contracts
{
    /// <summary>
    /// Interface for the Task Manager "Unit of Work"
    /// </summary>
    public interface ITaskManagerUow
    {
        // Save pending changes to the data store.
        void Commit();

        // Repositories
        IDictionaryRepository Dictionaries { get; }
        ITicketRepository Tickets { get; }
        ISqlRepositoryEntityFrameworkDisconnected<Comment> Comments { get; }
        IContrahentRepository Contrahents { get; }
        IEmployeeRepository Employees { get; }
        ISqlRepositoryEntityFrameworkDisconnected<UserDataModel> UserData { get; }
    }
}
