using System.Linq;
using TaskManager.BLL.Entities.DTO;

namespace TaskManager.DAL.Contracts
{
    public interface IEmployeeRepository
    {
        IQueryable<EmployeeDTO> FindDTOs(bool isOperator, bool orderByFullName = true);
    }
}