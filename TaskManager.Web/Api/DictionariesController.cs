using PDCore.Extensions;
using PDWebCore;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;

namespace TaskManager.Web.Api
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/dictionaries")]
    public class DictionariesController : ApiController
    {
        private readonly IDictionaryRepository dictionaryRepo;

        public DictionariesController(IDictionaryRepository dictionaryRepo)
        {
            this.dictionaryRepo = dictionaryRepo;
        }

        [Route]
        // GET: api/dictionaries
        public IHttpActionResult Get()
        {
            // Disallow fetching of all Dictionary objects
            return this.Forbid();
        }

        [Route("briefs")]
        public IQueryable<DictionaryBrief> GetBriefs()
        {
            return dictionaryRepo.FindAll<DictionaryBrief>();
        }

        // GET: api/dictionaries
        //public IQueryable<Dictionary> Get()
        //{
        //    return taskManagerUow.Dicionaries.FindAll();
        //}

        [Route("{id:int}", Name = "GetDictionary")]
        // GET: api/dictionaries/5
        [ResponseType(typeof(Dictionary))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var dictionary = await dictionaryRepo.FindByIdAsync(id);

            if (dictionary == null)
            {
                return NotFound();
            }

            return Ok(dictionary);
        }

        [Route]
        [ResponseType(typeof(Dictionary))]
        public async Task<IHttpActionResult> Get(string name, string value = null)
        {
            var dictionaries = await dictionaryRepo.GetAsync(name, value);

            if (dictionaries.IsEmpty())
            {
                return NotFound();
            }

            return Ok(dictionaries);
        }

        // GET: api/dictionaries/briefs?name=Status&value=Nowe
        [Route("briefs")]
        [ResponseType(typeof(DictionaryBrief))]
        public async Task<IHttpActionResult> GetBriefs(string name, string value = null)
        {
            var dictionaries = await dictionaryRepo.GetBriefsAsync(name, value);

            if (dictionaries.IsEmpty())
            {
                return NotFound();
            }

            return Ok(dictionaries);
        }

        // POST: api/dictionaries
        [Route]
        [ResponseType(typeof(Dictionary))]
        public async Task<IHttpActionResult> Post(Dictionary dictionary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dictionaryRepo.SaveNewAsync(dictionary);

            return CreatedAtRoute("GetDictionary", new { id = dictionary.Id }, dictionary);
        }

        // PUT: api/dictionaries/5
        //[ResponseType(typeof(void))]
        [Route("{id:int}")]
        [ResponseType(typeof(Dictionary))]
        public async Task<IHttpActionResult> Put(int id, Dictionary dictionary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dictionary.Id)
            {
                return BadRequest();
            }

            bool success = await dictionaryRepo.SaveUpdatedWithOptimisticConcurrencyAsync(dictionary, ModelState.AddModelError);

            if (success)
            {
                var result = await dictionaryRepo.FindByIdAsync(id);

                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/dictionaries/5
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var dictionary = await dictionaryRepo.FindByIdAsync(id);

            if (dictionary == null)
            {
                return NotFound();
            }

            bool success = await dictionaryRepo.DeleteAndCommitWithOptimisticConcurrencyAsync(dictionary, ModelState.AddModelError);

            if (success)
            {
                return this.NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}
