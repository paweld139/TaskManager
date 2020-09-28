using PDCore.Handlers;
using System.Collections;
using System.Collections.Generic;
using TaskManager.BLL.Commands.Statuses;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Enums;

namespace TaskManager.BLL.Handlers
{
    public class TicketStatusHandler : Handler2<ICollection<Status>>
    {
        public TicketStatusHandler(params IReceiver<ICollection<Status>>[] receivers) : base(receivers)
        {
        }
    }
}
