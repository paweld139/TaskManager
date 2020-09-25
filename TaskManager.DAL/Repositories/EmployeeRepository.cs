using AutoMapper;
using PDCore.Interfaces;
using PDCoreNew.Context.IContext;
using PDCoreNew.Repositories.Repo;
using System.Linq;
using TaskManager.BLL.Entities.DTO;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Repositories
{
    public class EmployeeRepository : SqlRepositoryEntityFrameworkDisconnected<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IEntityFrameworkDbContext ctx, ILogger logger, IMapper mapper) : base(ctx, logger, mapper)
        {
        }

        public IQueryable<EmployeeDTO> FindDTOs(bool? isOperator = null, bool orderByFullName = true)
        {
            var query = isOperator != null ? Find<EmployeeDTO>(e => e.Contrahent.IsOperator == isOperator.Value) : FindAll<EmployeeDTO>();

            if (orderByFullName)
            {
                query = query.OrderBy(c => c.FullName);
            }

            return query;
        }
    }
}
