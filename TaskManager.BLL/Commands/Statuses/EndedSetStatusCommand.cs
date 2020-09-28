using System.Security.Principal;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Enums;

namespace TaskManager.BLL.Commands.Statuses
{
    public class EndedSetStatusCommand : SetStatusCommand
    {
        public EndedSetStatusCommand(TicketBrief ticket, IPrincipal principal)
          : base(ticket, principal, Status.Ended)
        {
        }

        public override bool CanExecute()
        {
            return IsCustomer && actualStatus == Status.Receipt;
        }
    }
}
