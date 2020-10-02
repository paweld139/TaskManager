using PDCore.Extensions;
using PDCore.Strategies;
using PDCoreNew.Extensions;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TaskManager.DAL.Strategies
{
    public abstract class TaskManagerDataAccessStrategy<TEntity> : DataAccessStrategy<TEntity>
    {
        protected readonly IPrincipal principal;

        protected TaskManagerDataAccessStrategy(IPrincipal principal)
        {
            this.principal = principal;
        }

        protected bool HasRole => principal.Identity.GetRoles().Any();

        protected string EmployeeId => principal.Identity.GetEmployeeId();

        protected int ContrahentId => principal.Identity.GetContrahentId().ParseAsNullableInteger().GetValueOrDefault();


        protected bool IsCustomer => principal.IsInRole("Klient");

        protected bool IsOperator => principal.IsInRole("Serwisant");

        protected bool IsAdmin => principal.IsInRole("Admin");

        protected override bool NoRestrictions() => IsAdmin;

        public override bool CanUpdateAllProperties(TEntity entity) => NoRestrictions();

        public override bool CanDelete(TEntity entity) => NoRestrictions();

        public override Task AfterAdd(params object[] args)
        {
            return Task.CompletedTask;
        }
    }
}
