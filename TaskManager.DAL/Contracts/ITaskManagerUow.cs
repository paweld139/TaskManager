using PDCore.Interfaces;
using PDCore.Repositories.IRepo;
using PDCoreNew.Repositories.IRepo;
using PDWebCore.Models;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Contracts
{
    /// <summary>
    /// Interface for the Task Manager "Unit of Work"
    /// </summary>
    public interface ITaskManagerUow : IUnitOfWork
    {
        // Repositories
        IDictionaryRepository Dictionaries { get; }
        ITicketRepository Tickets { get; }
        ICommentRepository Comments { get; }
        IContrahentRepository Contrahents { get; }
        IEmployeeRepository Employees { get; }
        ISqlRepositoryEntityFrameworkDisconnected<UserDataModel> UserData { get; }
        IFileRepository FilesBase { get; }
        ISqlRepositoryEntityFrameworkDisconnected<File> Files { get; }
    }
}
