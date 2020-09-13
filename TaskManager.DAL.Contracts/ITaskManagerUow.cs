using PDCore.Repositories.IRepo;
using TaskManager.BLL.Models;

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
        ISqlRepositoryEntityFrameworkDisconnected<Dictionary> Dicionaries { get; }
        ISqlRepositoryEntityFrameworkDisconnected<Ticket> Tickets { get; }
    }
}
