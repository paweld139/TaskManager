using System.Linq;
using TaskManager.BLL.Entities.Briefs;

namespace TaskManager.DAL.Contracts
{
    public interface IContrahentRepository
    {
        IQueryable<ContrahentBrief> FindBriefs(bool orderByName = true);
    }
}