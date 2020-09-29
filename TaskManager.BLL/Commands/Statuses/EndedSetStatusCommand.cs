using System;
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
            return IsCustomer && statusBeforeSet == Status.Receipt;
        }

        public override void Execute()
        {
            ticketToEdit.ReceiptDate = DateTime.UtcNow;

            base.Execute();
        }

        public override void Undo()
        {
            ticketToEdit.ReceiptDate = null;

            base.Undo();
        }
    }
}
