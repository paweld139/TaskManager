using PDCore.Extensions;
using PDCore.Strategies;
using PDCoreNew.Extensions;
using System.Security.Principal;

namespace TaskManager.DAL.Strategies
{
    public abstract class TaskManagerDataAccessStrategy<TEntity> : DataAccessStrategy<TEntity>
    {
        protected readonly IPrincipal principal;

        protected TaskManagerDataAccessStrategy(IPrincipal principal)
        {
            this.principal = principal;
        }

        protected string EmployeeId => principal.Identity.GetEmployeeId();

        protected int ContrahentId => principal.Identity.GetContrahentId().ParseAsNullableInteger().GetValueOrDefault();

        protected bool IsCustomer => principal.IsInRole("Klient");


        protected override bool NoRestrictions() => principal.IsInRole("Admin");

        public override bool CanUpdateAllProperties(TEntity entity) => NoRestrictions();

        public override bool CanDelete(TEntity entity) => NoRestrictions();
    }
}
