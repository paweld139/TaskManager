using PDWebCore;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManager.DAL.Contracts;

namespace TaskManager.Web.Api
{
    [Authorize]
    public class FilesController : ApiController
    {
        private readonly ITaskManagerUow taskManagerUow;

        public FilesController(ITaskManagerUow taskManagerUow)
        {
            this.taskManagerUow = taskManagerUow;
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var file = await taskManagerUow.Files.FindByIdAsync(id);

            if (file == null)
            {
                return NotFound();
            }

            bool success = await taskManagerUow.Files.DeleteAndCommitWithOptimisticConcurrencyAsync(file, User, ModelState.AddModelError);

            if (success)
            {
                await taskManagerUow.FilesBase.RemoveFilelocal(id);

                return this.NoContent();
            }

            return BadRequest(ModelState);
        }
    }
}
