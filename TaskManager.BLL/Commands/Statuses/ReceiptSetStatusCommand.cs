using System;
using System.Security.Principal;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Enums;

namespace TaskManager.BLL.Commands.Statuses
{
    public class ReceiptSetStatusCommand : SetStatusCommand
    {
        private readonly DateTime? executionDateBeforeSet;

        public ReceiptSetStatusCommand(TicketBrief ticketToEdit, IPrincipal principal)
         : base(ticketToEdit, principal, Status.Receipt)
        {
            executionDateBeforeSet = ticketToEdit.ExecutionDate;
        }

        public override bool CanExecute()
        {
            return !IsCustomer
                   && ticketToEdit.OperatorId == EmployeeId
                   && (statusBeforeSet == Status.New || statusBeforeSet == Status.Rejected);
        }

        public override void Execute()
        {
            ticketToEdit.ExecutionDate = DateTime.UtcNow;

            base.Execute();
        }

        public override void Undo()
        {
            ticketToEdit.ExecutionDate = executionDateBeforeSet; //Możliwe, że data wykonania została ustawiona wcześniej.
                                                                 //Ma to miejsce wtedy, gdy zadanie jest oznaczone jako odrzucone.

            base.Undo();
        }

        //                    switch (status)
        //            {
        //                case Status.New:
        //                    statuses = new List<Status>
        //                    {
        //                        Status.Canceled, //Każdy może
        //                        Status.Clarify, //Klient nie może
        //                        Status.Receipt //Klient nie może
        //    };
        //                    break;
        //                case Status.Receipt:
        //                    statuses = new List<Status>
        //                    { 
        //                        Status.Canceled, //Klient nie może
        //                        Status.Ended, //Klient może
        //                        Status.Rejected //Klient może
        //};
        //break;
        //                case Status.Clarify:
        //                    statuses = new List<Status>
        //                    {
        //                        Status.Canceled, //Klient nie może
        //                        Status.New //Tylko automatycznie jak klient doda komentarz
        //                    };
        //break;
        //                case Status.Canceled: //nic
        //                    break;
        //                case Status.Rejected:
        //                    statuses = new List<Status>
        //                    {
        //                        Status.Canceled, //Klient nie może
        //                        Status.Receipt //Klient nie może
        //                    };
        //break;
        //                case Status.Ended: //Nic
        //                    break;
        //default:
        //                    break;
        //            }
    }
}
