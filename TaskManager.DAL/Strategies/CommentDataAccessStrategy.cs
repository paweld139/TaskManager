using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Basic;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Strategies
{
    public class CommentDataAccessStrategy : TaskManagerDataAccessStrategy<Comment>
    {
        public CommentDataAccessStrategy(IPrincipal principal) : base(principal)
        {
        }

        public override bool CanDelete(Comment entity)
        {
            return NoRestrictions() || EmployeeId == entity.EmployeeId;
        }

        public override Task<bool> CanAdd(params object[] args)
        {
            bool result = false;

            if (args[0] is Ticket ticket)
            {
                result = ticket.ContrahentId == ContrahentId;
            }

            result = NoRestrictions() || result;

            return Task.FromResult(result);
        }

        public override void PrepareForAdd(params object[] args)
        {
            if (args[1] is CommentBasic entity
                && args[0] is Ticket ticket)
            {
                entity.TicketId = ticket.Id;
                entity.EmployeeId = EmployeeId;
            }
        }

        public override bool CanUpdate(Comment entity)
        {
            throw new NotImplementedException();
        }

        public override ICollection<string> GetPropertiesForUpdate(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
