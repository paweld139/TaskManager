using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Basic;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Strategies
{
    public class ApplicationUserDataAccessStrategy : TaskManagerDataAccessStrategy<ApplicationUser>
    {
        private readonly ITaskManagerUow taskManagerUow;

        public ApplicationUserDataAccessStrategy(IPrincipal principal, ITaskManagerUow taskManagerUow) : base(principal)
        {
            this.taskManagerUow = taskManagerUow;
        }

        public override Task<bool> CanAdd(params object[] args)
        {
            Task<bool> result = Task.FromResult(false);

            if(args[0] is EmployeeBasic employee && args[1] is ContrahentBasic contrahent)
            {
                result = taskManagerUow.Contrahents.Find(c => c.Id == employee.ContrahentId
                                                                            && c.NIP == contrahent.NIP
                                                                            && c.LicenseKey == contrahent.LicenseKey
                                                                            && c.IsOperator == contrahent.IsOperator).AnyAsync();
            }

            return result;
        }

        public override bool CanUpdate(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public override ICollection<string> GetPropertiesForUpdate(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public override void PrepareForAdd(params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
