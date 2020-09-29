using PDCore.Commands;
using PDCore.Extensions;
using PDCore.Handlers;
using PDCore.Utils;
using PDCoreNew.Extensions;
using System.Collections.Generic;
using System.Security.Principal;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Enums;

namespace TaskManager.BLL.Commands.Statuses
{
    public abstract class SetStatusCommand : ICommand, IReceiver<ICollection<Status>>
    {
        protected readonly TicketBrief ticketToEdit;
        protected readonly Status statusBeforeSet;

        private readonly Status statusToSet;

        private readonly IPrincipal principal;

        protected SetStatusCommand(TicketBrief ticketToEdit, IPrincipal principal, Status statusToSet)
        {
            ObjectUtils.ThrowIfNull(ticketToEdit.GetTuple(nameof(ticketToEdit)), principal.GetTuple(nameof(principal)));

            this.ticketToEdit = ticketToEdit;
            statusBeforeSet = (Status)ticketToEdit.StatusId;

            this.statusToSet = statusToSet;

            this.principal = principal;
        }

        public abstract bool CanExecute();

        public virtual void Execute()
        {
            ticketToEdit.StatusId = (int)statusToSet;
        }

        public void Handle(ICollection<Status> request)
        {
            if (CanExecute())
            {
                request.Add(statusToSet);
            }
        }

        public virtual void Undo()
        {
            ticketToEdit.StatusId = (int)statusBeforeSet;
        }

        protected bool IsCustomer => principal.IsInRole("Klient");

        protected string EmployeeId => principal.Identity.GetEmployeeId();
    }
}
