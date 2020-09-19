using PDCore.Interfaces;
using PDCore.Repositories.IRepo;
using PDWebCore;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.DTO;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;

namespace TaskManager.Web.Api
{
    [Authorize]
    [RoutePrefix("api/tickets/{ticketId:int}/comments")]
    public class CommentsController : ApiController
    {
        private readonly ISqlRepositoryEntityFrameworkDisconnected<Comment> commentRepo;
        private readonly IDataAccessStrategy<Comment> dataAccessStrategy;
        private readonly ITicketRepository ticketRepo;

        public CommentsController(ISqlRepositoryEntityFrameworkDisconnected<Comment> commentRepo,
            IDataAccessStrategy<Comment> savingStrategy,
            ITicketRepository ticketRepo)
        {
            this.commentRepo = commentRepo;
            this.dataAccessStrategy = savingStrategy;
            this.ticketRepo = ticketRepo;
        }

        [Authorize(Roles = "Admin")]
        [Route]
        public IQueryable<CommentDTO> Get(int ticketId)
        {
            return commentRepo.Find<CommentDTO>(c => c.TicketId == ticketId).OrderByDescending(c => c.DateCreated);
        }

        [Authorize(Roles = "Admin")]
        [Route("{id:int}", Name = "GetComment")]
        [ResponseType(typeof(CommentDTO))]
        public async Task<IHttpActionResult> Get(int ticketId, int id)
        {
            _ = ticketId;

            var comment = await commentRepo.FindByIdAsync<CommentDTO>(id);

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

            var ticket = ticketRepo.FindByIdAsync(ticketId);

            if(ticket == null)
            {
                return NotFound();
            }

            bool result = await commentRepo.SaveNewAsync(comment, dataAccessStrategy, User, ticket);

            if(!result)
            {
                return this.Forbid();
            }

            return CreatedAtRoute("GetComment", new { id = comment.Id }, comment);
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var comment = await commentRepo.FindByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            bool success = await commentRepo.DeleteAndCommitWithOptimisticConcurrencyAsync(comment, ModelState.AddModelError);

            if (success)
            {
                return this.NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}
