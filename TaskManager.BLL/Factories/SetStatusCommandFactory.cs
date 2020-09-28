using PDCore.Extensions;
using PDCore.Factories.Fac;
using System;
using System.Security.Principal;
using TaskManager.BLL.Commands.Statuses;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Enums;

namespace TaskManager.BLL.Factories
{
    public class SetStatusCommandFactory : FactoryProvider<SetStatusCommand, SetStatusCommand>
    {
        public override SetStatusCommand CreateFactoryFor(params object[] parameters)
        {
            SetStatusCommand result = default;

            if (parameters[0] is TicketBrief ticketBrief && 
                parameters[1] is IPrincipal principal &&
                parameters[2] is int statusId)
            {
                string factoryName = statusId.CastObject<Status>().ToString();

                Type commandType = GetFactoryTypeFor(factoryName);

                result = (SetStatusCommand)Activator.CreateInstance(commandType, ticketBrief, principal);
            }

            return result;
        }
    }
}
