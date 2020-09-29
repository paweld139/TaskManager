using System.Security.Principal;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Enums;

namespace TaskManager.BLL.Commands.Statuses
{
    public class CanceledSetStatusCommand : SetStatusCommand
    {
        public CanceledSetStatusCommand(TicketBrief ticket, IPrincipal principal)
            : base(ticket, principal, Status.Canceled)
        {
        }

        public override bool CanExecute()
        {
            return statusBeforeSet == Status.New
                   || (!IsCustomer && (statusBeforeSet == Status.Receipt || statusBeforeSet == Status.Rejected || statusBeforeSet == Status.Clarify));
        }
    }
}
