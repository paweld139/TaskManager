using PDCore.Interfaces;
using PDCore.Repositories.IRepo;
using PDCoreNew.Extensions;
using PDCoreNew.Models;
using PDWebCore;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.DTO;
using TaskManager.BLL.Enums;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Services;

namespace TaskManager.Web.Api
{
    [Authorize]
    [RoutePrefix("api/tickets/{ticketId:int}/comments")]
    public class CommentsController : ApiController
    {
        private readonly ITaskManagerUow taskManagerUow;
        private readonly FileService fileService;

        public CommentsController(ITaskManagerUow taskManagerUow, FileService fileService)
        {
            this.taskManagerUow = taskManagerUow;
            this.fileService = fileService;
        }

        [Authorize(Roles = "Admin")]
        [Route]
        public IQueryable<CommentDTO> Get(int ticketId)
        {
            return taskManagerUow.Comments.Find<CommentDTO>(c => c.TicketId == ticketId).OrderByDescending(c => c.DateCreated);
        }

        [Authorize(Roles = "Admin")]
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
        [ResponseType(typeof(CommentDTO))]
        public async Task<IHttpActionResult> Post(int ticketId, [FromBody] CommentBasic model)
        {
            var ticket = await taskManagerUow.Tickets.FindByIdAsync(ticketId, false, false);

            if (ticket == null)
            {
                return NotFound();
            }

            bool success = await taskManagerUow.Comments.SaveNewAsync(model, User, args: ticket);

            if (success)
            {
                //await fileService.SaveFiles(model.Files, model, ObjType.Child);

                var comment = await taskManagerUow.Comments.FindByIdAsync<CommentDTO>(model.Id);

                return CreatedAtRoute("GetComment", new { id = comment.Id }, comment);
            }

            return this.Forbid();
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var comment = await taskManagerUow.Comments.FindByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            bool success = await taskManagerUow.Comments.DeleteAndCommitWithOptimisticConcurrencyAsync(comment, User, ModelState.AddModelError);

            if (success)
            {
                return this.NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}
