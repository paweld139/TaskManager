using PDCore.Repositories.IRepo;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManager.BLL.Models;
using TaskManager.DAL.Contracts;

namespace TaskManager.Web.Api
{
    public class DictionaryController : ApiController
    {
        private readonly ITaskManagerUow taskManagerUow;

        public DictionaryController(ITaskManagerUow taskManagerUow)
        {
            this.taskManagerUow = taskManagerUow;
        }

        //public IHttpActionResult Get()
        //{
        //    // Disallow fetching of all Dictionary objects
        //    return this.Forbid();
        //}

        public IQueryable<Dictionary> Get()
        {
            return taskManagerUow.Dicionaries.FindAll();
        }

        [ResponseType(typeof(Dictionary))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var dictionary = await taskManagerUow.Dicionaries.FindByIdAsync(id);

            if (dictionary == null)
            {
                return NotFound();
            }

            return Ok(dictionary);
        }
    }
}
