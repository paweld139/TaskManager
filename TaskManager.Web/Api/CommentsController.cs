using Microsoft.AspNet.Identity;
using PDCore.Extensions;
using PDWebCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManager.BLL.Entities;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.DTO;
using TaskManager.DAL.Contracts;

namespace TaskManager.Web.Api
{
    [RoutePrefix("api/tickets/{ticketId:int}/comments")]
    public class CommentsController : ApiController
    {
        private readonly ITaskManagerUow taskManagerUow;

        public CommentsController(ITaskManagerUow taskManagerUow)
        {
            this.taskManagerUow = taskManagerUow;
        }

        [Route]
        public IQueryable<CommentDTO> Get(int ticketId)
        {
            return taskManagerUow.Comments.Find<CommentDTO>(c => c.TicketId == ticketId).OrderByDescending(c => c.DateCreated);
        }

        [Route("{id:int}", Name = "GetComment")]
        [ResponseType(typeof(CommentDTO))]
        public async Task<IHttpActionResult> Get(int ticketId, int id)
        {
            _ = ticketId;

            var comment = await taskManagerUow.Comments.FindByIdAsync<CommentDTO>(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [Route]
        [ResponseType(typeof(CommentBasic))]
        public async Task<IHttpActionResult> Post(int ticketId, CommentBasic comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            comment.TicketId = ticketId;
            comment.EmployeeId = User.Identity.GetEmployeeId().ConvertObject<int>();

            await taskManagerUow.Comments.SaveNewAsync(comment);

            return CreatedAtRoute("GetComment", new { id = comment.Id }, comment);
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var comment = await taskManagerUow.Comments.FindByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            int employeeId = User.Identity.GetEmployeeId().ConvertObject<int>();

            if(employeeId != comment.EmployeeId)
            {
                return this.Forbid();
            }

            bool success = await taskManagerUow.Comments.DeleteAndCommitWithOptimisticConcurrencyAsync(comment, ModelState.AddModelError);

            if (success)
            {
                return this.NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}
