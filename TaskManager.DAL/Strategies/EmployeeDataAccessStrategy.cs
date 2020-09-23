using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Basic;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Strategies
{
    public class EmployeeDataAccessStrategy : TaskManagerDataAccessStrategy<Employee>
    {
        public EmployeeDataAccessStrategy(IPrincipal principal) : base(principal)
        {
        }

        public override Task<bool> CanAdd(params object[] args)
        {
            bool result = false;

            if (args[0] is ApplicationUser user)
            {
                result = !string.IsNullOrWhiteSpace(user.Id); 
            }

            result = NoRestrictions() || result;

            return Task.FromResult(result);
        }

        public override bool CanUpdate(Employee entity) => NoRestrictions();

        public override ICollection<string> GetPropertiesForUpdate(Employee entity)
        {
            throw new NotImplementedException();
        }

        public override void PrepareForAdd(params object[] args)
        {
            if(args[0] is ApplicationUser user && args[1] is EmployeeBasic employee)
            {
                employee.Id = user.Id;
            }
        }

        public override IQueryable<Employee> PrepareQuery(IQueryable<Employee> entities)
        {
            if(IsCustomer)
            {
                entities = entities.Where(e => e.ContrahentId == ContrahentId && !e.Contrahent.IsOperator);
            }

            return entities;
        }
    }
}
