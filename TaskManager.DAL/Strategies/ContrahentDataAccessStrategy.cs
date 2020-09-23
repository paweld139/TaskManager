using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Strategies
{
    public class ContrahentDataAccessStrategy : TaskManagerDataAccessStrategy<Contrahent>
    {
        public ContrahentDataAccessStrategy(IPrincipal principal) : base(principal)
        {
        }

        public override Task<bool> CanAdd(params object[] args) => Task.FromResult(NoRestrictions());

        public override bool CanUpdate(Contrahent entity) => NoRestrictions();

        public override ICollection<string> GetPropertiesForUpdate(Contrahent entity)
        {
            throw new NotImplementedException();
        }

        public override void PrepareForAdd(params object[] args)
        {
            if(args[0] is Contrahent contrahent)
            {
                contrahent.Archived = false;
            }
        }

        public override IQueryable<Contrahent> PrepareQuery(IQueryable<Contrahent> entities)
        {
            entities = entities.Where(c => !c.Archived);

            if(IsCustomer)
            {
                entities = entities.Where(c => c.Id == ContrahentId && !c.IsOperator);
            }

            return entities;
        }
    }
}
