using System.Linq;
using System.Web.Http;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Entities.DTO;
using TaskManager.BLL.Models;
using TaskManager.DAL.Contracts;

namespace TaskManager.Web.Api
{
    [Authorize]
    public class LookupsController : ApiController
    {
        private readonly ITaskManagerUow taskManagerUow;

        public LookupsController(ITaskManagerUow taskManagerUow)
        {
            this.taskManagerUow = taskManagerUow;
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
        public IQueryable<DictionaryBrief> GetStatuses()
        {
            return taskManagerUow.Dictionaries.FindBriefs("Status");
        }

        [ActionName("contrahents")]
        public IQueryable<ContrahentBrief> GetContrahents()
        {
            return taskManagerUow.Contrahents.FindBriefs();
        }


        [ActionName("representatives")]
        public IQueryable<EmployeeDTO> GetRepresentatives()
        {
            return taskManagerUow.Employees.FindDTOs(false);
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
        public Lookups GetLookups()
        {
            var lookups = new Lookups
            {
                Types = GetTypes().ToList(),
                Priorities = GetPriorities().ToList(),
                Statuses = GetStatuses().ToList(),
                Contrahents = GetContrahents().ToList(),
                Representatives = GetRepresentatives().ToList(),
                Operators = GetOperators().ToList()
            };

            return lookups;
        }
    }
}
