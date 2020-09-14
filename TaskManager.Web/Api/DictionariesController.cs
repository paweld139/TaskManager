using Autofac.Core.Activators.Reflection;
using AutoMapper;
using Microsoft.Web.Http;
using PDCore.Extensions;
using PDCore.Repositories.IRepo;
using PDWebCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using TaskManager.BLL.Models;
using TaskManager.DAL.Contracts;

namespace TaskManager.Web.Api
{
    public class DictionariesController : ApiController
    {
        private readonly ITaskManagerUow taskManagerUow;

        public DictionariesController(ITaskManagerUow taskManagerUow)
        {
            this.taskManagerUow = taskManagerUow;
        }

        // GET: api/dictionaries
        public IHttpActionResult Get()
        {
            // Disallow fetching of all Dictionary objects
            return this.Forbid();
        }

        //public IQueryable<Dictionary> Get()
        //{
        //    return taskManagerUow.Dicionaries.FindAll();
        //}

        // GET: api/dictionaries/5
        [ResponseType(typeof(Dictionary))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var dictionary = await taskManagerUow.Dictionaries.FindByIdAsync(id);

            if (dictionary == null)
            {
                return NotFound();
            }

            return Ok(dictionary);
        }

        // GET: api/dictionaries?name=Status&value=Nowe
        [ResponseType(typeof(DictionaryBrief))]
        public async Task<IHttpActionResult> Get(string name, string value = null)
        {
            var dictionaries = await taskManagerUow.Dictionaries.GetDictionaryBriefsAsync(name, value);

            if (dictionaries.IsEmpty())
            {
                return NotFound();
            }

            return Ok(dictionaries);
        }

        // POST: api/dictionaries
        [ResponseType(typeof(Dictionary))]
        public async Task<IHttpActionResult> Post(Dictionary dictionary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await taskManagerUow.Dictionaries.SaveNewAsync(dictionary);

            return CreatedAtRoute(WebApiConfig.DefaultApi, new { id = dictionary.Id }, dictionary);
        }

        // PUT: api/dictionaries/5
        [ResponseType(typeof(void))]
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

            bool success = await taskManagerUow.Dictionaries.SaveUpdatedWithOptimisticConcurrencyAsync(dictionary, ModelState.AddModelError);

            if (success)
            {
                return Ok(dictionary);
            }

            if (!taskManagerUow.Dictionaries.Exists(id))
            {
                return NotFound();
            }

            return BadRequest(ModelState);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var dictionary = await taskManagerUow.Dictionaries.FindByIdAsync(id);

            if(dictionary == null)
            {
                return NotFound();
            }

            bool success = await taskManagerUow.Dictionaries.DeleteAndCommitWithOptimisticConcurrencyAsync(dictionary, ModelState.AddModelError);

            if(success)
            {
                return this.NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}
