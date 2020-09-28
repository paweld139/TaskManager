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
            return actualStatus == Status.New
                   || (!IsCustomer && (actualStatus == Status.Receipt || actualStatus == Status.Rejected || actualStatus == Status.Clarify));
        }
    }
}
