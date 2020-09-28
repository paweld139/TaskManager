using System.Security.Principal;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Enums;

namespace TaskManager.BLL.Commands.Statuses
{
    public class ClarifySetStatusCommand : SetStatusCommand
    {
        public ClarifySetStatusCommand(TicketBrief ticket, IPrincipal principal)
          : base(ticket, principal, Status.Clarify)
        {
        }

        public override bool CanExecute()
        {
            return !IsCustomer && actualStatus == Status.New;
        }
    }
}
