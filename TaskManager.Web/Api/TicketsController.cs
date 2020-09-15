using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManager.BLL.Entities;
using TaskManager.BLL.Models;
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

        public TicketsController(ITaskManagerUow taskManagerUow)
        {
            this.taskManagerUow = taskManagerUow;
        }

        [Route]
        public IQueryable<Ticket> Get(bool includeSub = false)
        {
            return taskManagerUow.Tickets.Find(includeSub);
        }

        [Route("briefs")]
        public IEnumerable<TicketModel> GetBriefs()
        {
            return taskManagerUow.Tickets.GetAll<TicketModel>();
        }

        [Route("{id:int}", Name = "GetTicket")]
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

        [Route]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> Get(string number, bool includeSub = false)
        {
            var ticket = await taskManagerUow.Tickets.FindByNumberAsync(number, includeSub);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [Route("briefs/getByDate")]
        public IQueryable<TicketModel> GetByDate(DateTime? createDate)
        {
            return taskManagerUow.Tickets.FindByDateCreated<TicketModel>(createDate.Value.Date, createDate.Value.Date);
        }

        [Route("briefs/getByDate")]
        public IQueryable<TicketModel> Get(DateTime dateFrom, DateTime dateTo)
        {
            return taskManagerUow.Tickets.FindByDateCreated<TicketModel>(dateFrom.Date, dateTo.Date);
        }

        [Route("briefs/getByPage")]
        public IQueryable<TicketModel> Get(int page, int size)
        {
            return taskManagerUow.Tickets.FindPage<TicketModel>(page, size);
        }

        [Route("briefs/getBySubject")]
        public IQueryable<TicketModel> Get(string subject)
        {
            return taskManagerUow.Tickets.FindByFilter<TicketModel>(t => t.Subject, subject);
        }

        [Route("getKVP")]
        public IEnumerable<KeyValuePair<int, string>> GetVP(bool sortByValue = true)
        {
            return taskManagerUow.Tickets.GetKeyValuePairs(t => t.Id, t => t.Subject, sortByValue);
        }

        [Route]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> Post(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await taskManagerUow.Tickets.SaveNewAsync(ticket);

            return CreatedAtRoute("GetTicket", new { id = ticket.Id }, ticket);
        }
    }
}
