using PDCore.Extensions;
using PDCore.Interfaces;
using PDCoreNew.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.Simple;
using TaskManager.BLL.Enums;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Strategies
{
    public class TicketDataAccessStrategy : TaskManagerDataAccessStrategy<Ticket>
    {
        private readonly IDataAccessStrategy<File> fileDataAccessStrategy;

        public TicketDataAccessStrategy(IPrincipal principal, IDataAccessStrategy<File> fileDataAccessStrategy) : base(principal)
        {
            this.fileDataAccessStrategy = fileDataAccessStrategy;
        }

        public override bool CanUpdate(Ticket entity)
        {
            bool result = !IsCustomer || (IsCustomer && entity.ContrahentId == ContrahentId);

            return result || NoRestrictions();
        }

        public override ICollection<string> GetPropertiesForUpdate(Ticket entity)
        {
            var properties = new List<string>
            {
                nameof(entity.PriorityId),
                nameof(entity.Budget),
                nameof(entity.OperatorId)
            };

            if (IsCustomer)
            {
                properties.RemoveRange(1, 2);
            }
            else
            {
                properties.RemoveAt(0);
            }

            return properties;
        }

        public override Task<bool> CanAdd(params object[] args)
        {
            bool result = HasRole && EmployeeId != null && ContrahentId > 0;

            return Task.FromResult(result);
        }

        public override void PrepareForAdd(params object[] args)
        {
            if (args[0] is TicketSimple ticket)
            {
                if (IsCustomer) //Serwisanci mogą tworzyć zadanie dla wybranego kontrahenta i przedstawiciela
                {
                    ticket.ContrahentId = ContrahentId;
                    ticket.RepresentativeId = EmployeeId;
                }

                if (!ticket.Files.IsEmpty())
                {
                    ticket.Files.Where(f => fileDataAccessStrategy.CanAdd(ticket, ObjType.Parent, f).Result)
                          .ForEach(f => fileDataAccessStrategy.PrepareForAdd(ticket, ObjType.Parent, f));
                }

                ticket.StatusId = (int)Status.New;

                ticket.ExecutionDate = null;
                ticket.ReceiptDate = null;
                ticket.OperatorId = null;
                ticket.Budget = null;
            }
        }

        public override IQueryable<Ticket> PrepareQuery(IQueryable<Ticket> entities)
        {
            if (IsCustomer)
            {
                entities = entities.Where(e => e.ContrahentId == ContrahentId);
            }

            return entities;
        }
    }
}
