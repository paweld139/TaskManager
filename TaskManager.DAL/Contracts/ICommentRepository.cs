using PDCore.Repositories.IRepo;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Contracts
{
    public interface ICommentRepository : ISqlRepositoryEntityFrameworkDisconnected<Comment>
    {
        IQueryable<Comment> Find(bool includeFiles, bool asNoTracking = true);
        Task<Comment> FindByIdAsync(int id, bool includeFiles, bool asNoTracking = true);
    }
}
