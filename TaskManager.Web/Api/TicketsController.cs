using AutoMapper;
using Microsoft.Web.Http;
using PDWebCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManager.BLL.Entities;
using TaskManager.BLL.Entities.Details;
using TaskManager.BLL.Entities.DTO;
using TaskManager.DAL.Contracts;

namespace TaskManager.Web.Api
{
    [ApiVersion("1.0")]
    //[ApiVersion("1.1")]
    //[RoutePrefix("api/v{version:apiVersion}/tasks")]
    [RoutePrefix("api/tickets")]
    public class TicketsController : ApiController
    {
        private readonly ITaskManagerUow taskManagerUow;
        private readonly IMapper mapper;

        public TicketsController(ITaskManagerUow taskManagerUow, IMapper mapper)
        {
            this.taskManagerUow = taskManagerUow;
            this.mapper = mapper;
        }

        [Route("admin")]
        public IQueryable<Ticket> Get(bool includeSub = false)
        {
            return taskManagerUow.Tickets.Find(includeSub);
        }

        [Route]
        public IQueryable<TicketDetails> Get()
        {
            return taskManagerUow.Tickets.FindAll<TicketDetails>();
        }

        [Route("briefs")]
        public IQueryable<TicketDTO> GetBriefs()
        {
            return taskManagerUow.Tickets.FindAll<TicketDTO>();
        }

        [Route("admin/{id:int}", Name = "GetTicketAdmin")]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> Get(int id, bool includeSub = false)
        {
            var ticket = await taskManagerUow.Tickets.FindByIdAsync(id, includeSub);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [Route("{id:int}", Name = "GetTicket")]
        [ResponseType(typeof(TicketDetails))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var ticket = await taskManagerUow.Tickets.FindDetailsByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [Route]
        [ResponseType(typeof(TicketDetails))]
        public async Task<IHttpActionResult> Get(string number)
        {
            var ticket = await taskManagerUow.Tickets.FindDetailsByNumberAsync(number);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [Route("briefs/getByDate")]
        public IQueryable<TicketDTO> GetByDate(DateTime createDate)
        {
            return taskManagerUow.Tickets.FindByDateCreated<TicketDTO>(createDate.Date, createDate.Date);
        }

        [Route("briefs/getByDate")]
        public IQueryable<TicketDTO> Get(DateTime dateFrom, DateTime dateTo)
        {
            return taskManagerUow.Tickets.FindByDateCreated<TicketDTO>(dateFrom.Date, dateTo.Date);
        }

        [Route("briefs/getByPage")]
        public IQueryable<TicketDTO> Get(int page, int size)
        {
            return taskManagerUow.Tickets.FindPage<TicketDTO>(page, size);
        }

        [Route("briefs/getBySubject")]
        public IQueryable<TicketDTO> GetBySubject(string subject)
        {
            return taskManagerUow.Tickets.FindByFilter<TicketDTO>(t => t.Subject, subject);
        }

        [Route("getKVP")]
        public IEnumerable<KeyValuePair<int, string>> GetVP(bool sortByValue = true)
        {
            return taskManagerUow.Tickets.GetKeyValuePairs(t => t.Id, t => t.Subject, sortByValue);
        }

        [Route("admin")]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> Post(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await taskManagerUow.Tickets.SaveNewAsync(ticket);

            return CreatedAtRoute("GetTicketAdmin", new { id = ticket.Id }, ticket);
        }

        [ResponseType(typeof(TicketDetails))]
        public async Task<IHttpActionResult> Post(TicketDetails ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await taskManagerUow.Tickets.SaveNewAsync(ticket);

            return CreatedAtRoute("GetTicket", new { id = ticket.Id }, ticket);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(TicketDetails))]
        public async Task<IHttpActionResult> Put(int id, TicketDetails details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != details.Id)
            {
                return BadRequest();
            }

            var ticket = await taskManagerUow.Tickets.FindByIdAsync(id, true, false);

            if(ticket == null)
            {
                return NotFound();
            }

            bool success = await taskManagerUow.Tickets.SaveUpdatedWithOptimisticConcurrencyAsync(details, ticket, User, ModelState.AddModelError);

            if (success)
            {
                return Ok(details);
            }

            if (!taskManagerUow.Tickets.Exists(id))
            {
                return NotFound();
            }

            return BadRequest(ModelState);
        }

        [Route("admin/{id:int}")]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> Put(int id, Ticket model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            bool success = await taskManagerUow.Tickets.SaveUpdatedWithOptimisticConcurrencyAsync(model, User, ModelState.AddModelError);

            if (success)
            {
                var ticket = await taskManagerUow.Tickets.FindByIdAsync(id, true);

                return Ok(ticket);
            }

            if (!taskManagerUow.Tickets.Exists(id))
            {
                return NotFound();
            }

            return BadRequest(ModelState);
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var ticket = await taskManagerUow.Tickets.FindByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            bool success = await taskManagerUow.Tickets.DeleteAndCommitWithOptimisticConcurrencyAsync(ticket, ModelState.AddModelError);

            if (success)
            {
                return this.NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}
