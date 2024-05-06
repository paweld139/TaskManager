using Microsoft.Web.Http;
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
using TaskManager.BLL.Entities.Simple;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Proxies;
using TaskManager.DAL.Services;

namespace TaskManager.Web.Api
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    //[RoutePrefix("api/v{version:apiVersion}/tasks")]
    [RoutePrefix("api/tickets")]
    public class TicketsController : ApiController
    {
        private readonly ITaskManagerUow taskManagerUow;
        private readonly TicketService ticketService;

        public TicketsController(ITaskManagerUow taskManagerUow, TicketService ticketService)
        {
            this.taskManagerUow = taskManagerUow;
            this.ticketService = ticketService;
        }

        [Authorize(Roles = "Admin")]
        [Route("admin")]
        public IQueryable<Ticket> Get(bool includeSub = false)
        {
            return taskManagerUow.Tickets.Find(includeSub);
        }

        [Authorize(Roles = "Admin")]
        [Route]
        public IQueryable<TicketDetails> Get()
        {
            return taskManagerUow.Tickets.FindAll<TicketDetails>();
        }

        [Route("briefs")]
        public IQueryable<TicketDTO> GetBriefs()
        {
            return taskManagerUow.Tickets.FindAll<TicketDTOProxy>();
        }

        [Authorize(Roles = "Admin")]
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
            var ticket = await taskManagerUow.Tickets.FindByIdAsync<TicketDetailsProxy>(id);

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
            return taskManagerUow.Tickets.FindByDateCreated<TicketDTOProxy>(createDate, createDate);
        }

        [Route("briefs/getByDate")]
        public IQueryable<TicketDTO> Get(DateTime dateFrom, DateTime dateTo)
        {
            return taskManagerUow.Tickets.FindByDateCreated<TicketDTOProxy>(dateFrom, dateTo);
        }

        [Route("briefs/getByPage")]
        public IQueryable<TicketDTO> Get(int page, int size)
        {
            return taskManagerUow.Tickets.FindPage<TicketDTOProxy>(page, size);
        }

        [Authorize(Roles = "Admin")]
        [Route("briefs/getBySubject")]
        public IQueryable<TicketDTO> GetBySubject(string subject)
        {
            return taskManagerUow.Tickets.FindByFilter<TicketDTO>(t => t.Subject, subject);
        }

        [Authorize(Roles = "Admin")]
        [Route("getKVP")]
        public IEnumerable<KeyValuePair<int, string>> GetKVP(bool sortByValue = true)
        {
            return taskManagerUow.Tickets.GetKeyValuePairs(t => t.Id, t => t.Subject, sortByValue);
        }

        [Authorize(Roles = "Admin")]
        [Route("admin")]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> Post(Ticket model)
        {
            await taskManagerUow.Tickets.SaveNewAsync(model);

            var ticket = await taskManagerUow.Tickets.FindByIdAsync(model.Id, true);

            return CreatedAtRoute("GetTicketAdmin", new { id = ticket.Id }, ticket);
        }

        [MapToApiVersion("1.0")]
        [ResponseType(typeof(TicketBasic))]
        public async Task<IHttpActionResult> Post([FromBody] TicketBasic model)
        {
            bool success = await taskManagerUow.Tickets.SaveNewAsync(model, User);

            if (success)
            {
                return CreatedAtRoute("GetTicket", new { id = model.Id }, model);
            }

            return this.Forbid();
        }

        [MapToApiVersion("1.1")]
        [ResponseType(typeof(TicketDetails))]
        //[ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> PostTicket(TicketSimple model)
        {
            bool success = await taskManagerUow.Tickets.SaveNewAsync(model, User);

            if (success)
            {
                await taskManagerUow.FilesBase.AddFilesLocal(model);

                var ticket = await taskManagerUow.Tickets.FindByIdAsync<TicketDetailsProxy>(model.Id);

                return CreatedAtRoute("GetTicket", new { id = ticket.Id }, ticket);
            }

            return this.Forbid();
        }

        [Route("{id:int}")]
        [ResponseType(typeof(TicketDetails))]
        //[ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> Put(int id, TicketBasic model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            bool success = await taskManagerUow.Tickets.SaveUpdatedWithOptimisticConcurrencyAsync(model, User, ModelState.AddModelError);

            if (success)
            {
                var ticket = await taskManagerUow.Tickets.FindByIdAsync<TicketDetailsProxy>(id);

                return Ok(ticket);
            }

            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [Route("admin/{id:int}")]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> Put(int id, Ticket model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            bool success = await taskManagerUow.Tickets.SaveUpdatedWithOptimisticConcurrencyAsync(model, User, ModelState.AddModelError);

            if (success)
            {
                var ticket = await taskManagerUow.Tickets.FindByIdAsync(id);

                return Ok(ticket);
            }

            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
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

        [HttpPatch]
        [Route("{id:int}/{statusId:int}")]
        [ResponseType(typeof(TicketDetails))]
        public async Task<IHttpActionResult> SetStatus(int id, int statusId)
        {
            var ticket = await taskManagerUow.Tickets.FindByIdAsync(id, false, false);

            if (ticket == null)
            {
                return NotFound();
            }

            ticketService.SetStatus(ticket, statusId);

            bool success = await taskManagerUow.Tickets.SaveUpdatedWithOptimisticConcurrencyAsync(ticket, ModelState.AddModelError);

            if (success)
            {
                var result = await taskManagerUow.Tickets.FindByIdAsync<TicketDetailsProxy>(id);

                return Ok(result);
            }

            return BadRequest(ModelState);
        }
    }
}
