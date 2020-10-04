using PDCore.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Factories;
using TaskManager.BLL.Processors;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Proxies;

namespace TaskManager.DAL.Services
{
    public class TicketService
    {
        private readonly ITaskManagerUow taskManagerUow;
        private readonly StatusProcessor statusProcessor;
        private readonly IPrincipal principal;
        private readonly SetStatusCommandFactory setStatusCommandFactory;
        private readonly CommandManager commandManager;

        public TicketService(ITaskManagerUow taskManagerUow, StatusProcessor statusProcessor, IPrincipal principal,
            SetStatusCommandFactory setStatusCommandFactory, CommandManager commandManager)
        {
            this.statusProcessor = statusProcessor;
            this.principal = principal;
            this.setStatusCommandFactory = setStatusCommandFactory;
            this.commandManager = commandManager;
            this.taskManagerUow = taskManagerUow;
        }

        public IQueryable<DictionaryBrief> GetAvailableStatuses(int? ticketId)
        {
            IQueryable<DictionaryBrief> result;

            if (ticketId == null)
            {
                result = taskManagerUow.Dictionaries.FindBriefs("Status");
            }
            else
            {
                var ticket = taskManagerUow.Tickets.FindById(ticketId.Value);

                var availableStatuses = statusProcessor.GetAvailableStatuses(ticket, principal);

                result = taskManagerUow.Dictionaries.Find<DictionaryBriefProxy>(d => availableStatuses.Contains(d.Id));
            }

            return result;
        }

        public bool SetStatus(Ticket ticket, int statusId)
        {
            bool result = true;

            var command = setStatusCommandFactory.CreateFactoryFor(ticket, principal, statusId);

            if (command == null)
            {
                result = false;
            }
            else
            {
                commandManager.Invoke(command);
            }

            return result;
        }
    }
}
