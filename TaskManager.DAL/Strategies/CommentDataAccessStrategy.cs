using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Enums;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Strategies
{
    public class CommentDataAccessStrategy : TaskManagerDataAccessStrategy<Comment>
    {
        public CommentDataAccessStrategy(IPrincipal principal) : base(principal)
        {
        }

        public override bool CanDelete(Comment entity)
        {
            return EmployeeId == entity.EmployeeId || NoRestrictions();
        }

        public override Task<bool> CanAdd(params object[] args)
        {
            bool result = false;

            if (args[0] is Ticket ticket)
            {
                result = !IsCustomer || ticket.ContrahentId == ContrahentId;
            }

            result = result || NoRestrictions();

            return Task.FromResult(result);
        }

        public override void PrepareForAdd(params object[] args)
        {
            if (args[1] is CommentBasic entity
                && args[0] is Ticket ticket)
            {
                entity.TicketId = ticket.Id;
                entity.EmployeeId = EmployeeId;

                if ((Status)ticket.StatusId == Status.Clarify && IsCustomer)
                {
                    ticket.StatusId = (int)Status.New;

                    //Jeśli ktoś anulował zadanie w międzyczasie, to nie chcemy było oznaczone jako nowe.
                    //Możliwe jednak, że to nie status został zmieniony. Nie chcemy również, by zadanie tkwiło jako do wyjaśnienia
                    //i wymagane było dodanie kolejnego komentarza przez klienta. Pozostanie więc client wins.
                }
            }
        }

        public override bool CanUpdate(Comment entity)
        {
            throw new NotImplementedException();
        }

        public override ICollection<string> GetPropertiesForUpdate(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
