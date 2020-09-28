using PDCore.Extensions;
using PDCore.Handlers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Enums;
using TaskManager.BLL.Factories;
using TaskManager.BLL.Translators;

namespace TaskManager.BLL.Processors
{
    public class StatusProcessor
    {
        private readonly SetStatusCommandFactory setStatusCommandFactory;

        public StatusProcessor(SetStatusCommandFactory setStatusCommandFactory)
        {
            this.setStatusCommandFactory = setStatusCommandFactory;
        }

        public IEnumerable<int> GetAvailableStatuses(TicketBrief ticketBrief, IPrincipal principal)
        {
            ICollection<Status> statuses = new List<Status>();

            var commands = setStatusCommandFactory.GetAllFactories(ticketBrief, principal).ToArray();

            var handler = new Handler2<ICollection<Status>>(commands);

            handler.Handle(statuses);

            return statuses.ConvertOrCastTo<Status, int>();
        }
    }
}
