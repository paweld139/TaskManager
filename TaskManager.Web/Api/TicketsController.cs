using Microsoft.Web.Http;
using PDCore.Interfaces;
using PDWebCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.Details;
using TaskManager.BLL.Entities.DTO;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Proxies;

namespace TaskManager.Web.Api
{
    [Authorize]
    [ApiVersion("1.0")]
    //[ApiVersion("1.1")]
    //[RoutePrefix("api/v{version:apiVersion}/tasks")]
    [RoutePrefix("api/tickets")]
    public class TicketsController : ApiController
    {
        private readonly ITicketRepository ticketRepo;
        private readonly IDataAccessStrategy<Ticket> dataAccessStrategy;

        public TicketsController(ITicketRepository ticketRepo, IDataAccessStrategy<Ticket> dataAccessStrategy)
        {
            this.ticketRepo = ticketRepo;
            this.dataAccessStrategy = dataAccessStrategy;
        }

        [Authorize(Roles = "Admin")]
        [Route("admin")]
        public IQueryable<Ticket> Get(bool includeSub = false)
        {
            return ticketRepo.Find(includeSub);
        }

        [Authorize(Roles = "Admin")]
        [Route]
        public IQueryable<TicketDetails> Get()
        {
            return ticketRepo.FindAll<TicketDetails>();
        }

        [Route("briefs")]
        public IQueryable<TicketDTO> GetBriefs()
        {
            return ticketRepo.FindAll<TicketDTOProxy>();
        }

        [Authorize(Roles = "Admin")]
        [Route("admin/{id:int}", Name = "GetTicketAdmin")]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> Get(int id, bool includeSub = false)
        {
            var ticket = await ticketRepo.FindByIdAsync(id, includeSub);

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
            var ticket = await ticketRepo.FindByIdAsync<TicketDetailsProxy>(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [Authorize(Roles = "Admin")]
        [Route]
        [ResponseType(typeof(TicketDetails))]
        public async Task<IHttpActionResult> Get(string number)
        {
            var ticket = await ticketRepo.FindDetailsByNumberAsync(number);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [Route("briefs/getByDate")]
        public IQueryable<TicketDTO> GetByDate(DateTime createDate)
        {
            return ticketRepo.FindByDateCreated<TicketDTOProxy>(createDate.Date, createDate.Date);
        }

        [Route("briefs/getByDate")]
        public IQueryable<TicketDTO> Get(DateTime dateFrom, DateTime dateTo)
        {
            return ticketRepo.FindByDateCreated<TicketDTOProxy>(dateFrom.Date, dateTo.Date);
        }

        [Route("briefs/getByPage")]
        public IQueryable<TicketDTO> Get(int page, int size)
        {
            return ticketRepo.FindPage<TicketDTOProxy>(page, size);
        }

        [Authorize(Roles = "Admin")]
        [Route("briefs/getBySubject")]
        public IQueryable<TicketDTO> GetBySubject(string subject)
        {
            return ticketRepo.FindByFilter<TicketDTO>(t => t.Subject, subject);
        }

        [Authorize(Roles = "Admin")]
        [Route("getKVP")]
        public IEnumerable<KeyValuePair<int, string>> GetKVP(bool sortByValue = true)
        {
            return ticketRepo.GetKeyValuePairs(t => t.Id, t => t.Subject, sortByValue);
        }

        [Authorize(Roles = "Admin")]
        [Route("admin")]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> Post(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await ticketRepo.SaveNewAsync(ticket);

            return CreatedAtRoute("GetTicketAdmin", new { id = ticket.Id }, ticket);
        }

        [ResponseType(typeof(TicketBasic))]
        public async Task<IHttpActionResult> Post(TicketBasic ticket) // TODO: Dodać walidację
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await ticketRepo.SaveNewAsync(ticket, dataAccessStrategy, User);

            if (!result)
            {
                return this.Forbid();
            }

            return CreatedAtRoute("GetTicket", new { id = ticket.Id }, ticket);
        }

        [Route("{id:int}")]
        [ResponseType(typeof(TicketDetails))]
        public async Task<IHttpActionResult> Put(int id, TicketBasic basic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != basic.Id)
            {
                return BadRequest();
            }

            var ticket = await ticketRepo.FindByIdAsync(id, true, false);

            if(ticket == null)
            {
                return NotFound();
            }

            var result = await ticketRepo.SaveUpdatedWithOptimisticConcurrencyAsync<TicketDetailsProxy>(basic, ticket, dataAccessStrategy, User, ModelState.AddModelError);

            if (ModelState.IsValid)
            {
                return Ok(result);
            }

            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
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

            bool success = await ticketRepo.SaveUpdatedWithOptimisticConcurrencyAsync(model, dataAccessStrategy, User, ModelState.AddModelError);

            if (success)
            {
                var ticket = await ticketRepo.FindByIdAsync(id, true);

                return Ok(ticket);
            }

            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var ticket = await ticketRepo.FindByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            bool success = await ticketRepo.DeleteAndCommitWithOptimisticConcurrencyAsync(ticket, ModelState.AddModelError);

            if (success)
            {
                return this.NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}
