using PDCore.Commands;
using PDCore.Handlers;
using PDCoreNew.Extensions;
using System.Collections.Generic;
using System.Security.Principal;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Enums;

namespace TaskManager.BLL.Commands.Statuses
{
    public abstract class SetStatusCommand : ICommand, IReceiver<ICollection<Status>>
    {
        protected readonly TicketBrief ticket;
        private readonly IPrincipal principal;
        private readonly Status statusToSet;
        protected readonly Status actualStatus;

        protected SetStatusCommand(TicketBrief ticket, IPrincipal principal, Status statusToSet)
        {
            this.ticket = ticket;
            actualStatus = (Status)ticket.StatusId;
            this.principal = principal;
            this.statusToSet = statusToSet;
        }

        public abstract bool CanExecute();

        public virtual void Execute()
        {
            ticket.StatusId = (int)statusToSet;
        }

        public void Handle(ICollection<Status> request)
        {
            if (CanExecute())
            {
                request.Add(statusToSet);
            }
        }

        public void Undo()
        {
            ticket.StatusId = (int)actualStatus;
        }

        protected bool IsCustomer => principal.IsInRole("Klient");

        protected string EmployeeId => principal.Identity.GetEmployeeId();
    }
}
