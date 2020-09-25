using PDCore.Repositories.IRepo;
using System.Linq;
using TaskManager.BLL.Entities.DTO;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Contracts
{
    public interface IEmployeeRepository : ISqlRepositoryEntityFrameworkDisconnected<Employee>
    {
        IQueryable<EmployeeDTO> FindDTOs(bool? isOperator = null, bool orderByFullName = true);
    }
}