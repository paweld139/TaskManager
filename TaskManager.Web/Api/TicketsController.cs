using AutoMapper;
using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
        public IQueryable<TicketModel> GetBriefs()
        {
            return taskManagerUow.Tickets.FindAll<TicketModel>();
        }

        [Route("{id:int}")]
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

        [Route("getByDate")]
        public IQueryable<Ticket> Get(DateTime createDate)
        {
            return taskManagerUow.Tickets.FindByDateCreated(createDate.Date, createDate.Date);
        }

        [Route("getByDate")]
        public IQueryable<Ticket> Get(DateTime dateFrom, DateTime dateTo)
        {
            return taskManagerUow.Tickets.FindByDateCreated(dateFrom.Date, dateTo.Date);
        }

        [Route("getByPage")]
        public IQueryable<Ticket> Get(int page, int size)
        {
            return taskManagerUow.Tickets.FindPage(page, size);
        }

        [Route("getBySubject")]
        public IQueryable<Ticket> Get(string subject)
        {
            return taskManagerUow.Tickets.FindByFilter(t => t.Subject, subject);
        }

        [Route("getKVP")]
        public IEnumerable<KeyValuePair<int, string>> GetVP()
        {
            return taskManagerUow.Tickets.GetKeyValuePairs(t => t.Id, t => t.Subject);
        }
    }
}
