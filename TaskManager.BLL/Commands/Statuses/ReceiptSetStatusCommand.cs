using System;
using System.Security.Principal;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Enums;

namespace TaskManager.BLL.Commands.Statuses
{
    public class ReceiptSetStatusCommand : SetStatusCommand
    {
        public ReceiptSetStatusCommand(TicketBrief ticket, IPrincipal principal)
         : base(ticket, principal, Status.Receipt)
        {
        }

        public override bool CanExecute()
        {
            return !IsCustomer
                   && ticket.OperatorId == EmployeeId
                   && (actualStatus == Status.New || actualStatus == Status.Rejected);
        }

        public override void Execute()
        {
            ticket.ExecutionDate = DateTime.UtcNow;

            base.Execute();
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
