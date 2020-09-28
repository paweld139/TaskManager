using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Entities.DTO;
using TaskManager.BLL.Models;
using TaskManager.BLL.Processors;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Proxies;

namespace TaskManager.Web.Api
{
    [Authorize]
    public class LookupsController : ApiController
    {
        private readonly ITaskManagerUow taskManagerUow;
        private readonly StatusProcessor statusProcessor;

        public LookupsController(ITaskManagerUow taskManagerUow, StatusProcessor statusProcessor)
        {
            this.taskManagerUow = taskManagerUow;
            this.statusProcessor = statusProcessor;
        }


        [ActionName("types")]
        public IQueryable<DictionaryBrief> GetTypes()
        {
            return taskManagerUow.Dictionaries.FindBriefs("Typ");
        }

        [ActionName("priorities")]
        public IQueryable<DictionaryBrief> GetPriorities()
        {
            return taskManagerUow.Dictionaries.FindBriefs("Priorytet");
        }

        [ActionName("statuses")]
        public IQueryable<DictionaryBrief> GetStatuses(int? ticketId = null)
        {
            IQueryable<DictionaryBrief> result;

            if (ticketId == null)
            {
                result =  taskManagerUow.Dictionaries.FindBriefs("Status");
            }
            else
            {
                var ticket = taskManagerUow.Tickets.FindById(ticketId.Value);

                var availableStatuses = statusProcessor.GetAvailableStatuses(ticket, User);

                result = taskManagerUow.Dictionaries.Find<DictionaryBriefProxy>(d => availableStatuses.Contains(d.Id));
            }

            return result;
        }

        [ActionName("contrahents")]
        public IQueryable<ContrahentBrief> GetContrahents()
        {
            return taskManagerUow.Contrahents.FindBriefs();
        }


        [ActionName("representatives")]
        public IQueryable<EmployeeDTO> GetRepresentatives()
        {
            return taskManagerUow.Employees.FindDTOs();
        }

        [ActionName("operators")]
        public IQueryable<EmployeeDTO> GetOperators()
        {
            return taskManagerUow.Employees.FindDTOs(true);
        }


        // Lookups: aggregates the many little lookup lists in one payload
        // to reduce roundtrips when the client launches.
        // GET: api/lookups
        [ActionName("all")]
        public Lookups GetLookups(int? ticketId = null)
        {
            var lookups = new Lookups
            {
                Types = GetTypes().ToList(),
                Priorities = GetPriorities().ToList(),
                Statuses = GetStatuses(ticketId).ToList(),
                Contrahents = GetContrahents().ToList(),
                Representatives = GetRepresentatives().ToList(),
                Operators = GetOperators().ToList()
            };

            return lookups;
        }
    }
}
