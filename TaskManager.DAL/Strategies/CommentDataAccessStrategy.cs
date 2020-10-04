using PDCore.Extensions;
using PDCore.Interfaces;
using PDCoreNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Details;
using TaskManager.BLL.Enums;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Strategies
{
    public class CommentDataAccessStrategy : TaskManagerDataAccessStrategy<Comment>
    {
        private readonly IDataAccessStrategy<File> fileDataAccessStrategy;

        public CommentDataAccessStrategy(IPrincipal principal, IDataAccessStrategy<File> fileDataAccessStrategy) : base(principal)
        {
            this.fileDataAccessStrategy = fileDataAccessStrategy;
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
            if (args[1] is CommentDetails entity
                && args[0] is Ticket ticket)
            {
                entity.TicketId = ticket.Id;
                entity.EmployeeId = EmployeeId;

                if(!entity.Files.IsEmpty())
                {
                    entity.Files.Where(f => fileDataAccessStrategy.CanAdd(entity, ObjType.Child, f).Result)
                          .ForEach(f => fileDataAccessStrategy.PrepareForAdd(entity, ObjType.Child, f));
                }

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
