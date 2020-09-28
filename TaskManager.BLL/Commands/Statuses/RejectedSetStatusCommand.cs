using System.Security.Principal;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Enums;

namespace TaskManager.BLL.Commands.Statuses
{
    public class RejectedSetStatusCommand : SetStatusCommand
    {
        public RejectedSetStatusCommand(TicketBrief ticket, IPrincipal principal)
         : base(ticket, principal, Status.Rejected)
        {
        }

        public override bool CanExecute()
        {
            return IsCustomer && actualStatus == Status.Receipt;
        }
    }
}
