using PDCore.Extensions;
using PDCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskManager.DAL.Contracts;

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly ITaskManagerUow taskManagerUow;

        public FileController(ITaskManagerUow taskManagerUow)
        {
            this.taskManagerUow = taskManagerUow;
        }

        public async Task<ActionResult> OpenFile(string id)
        {
            if (!int.TryParse(id, out int ids))
            {
                return View("_Error", model: Resources.ErrorMessages.AccessDenied);
            }

            var file = await taskManagerUow.Files.FindByIdAsync(ids);

            if (file == null)
            {
                return View("_Error", model: Resources.ErrorMessages.AccessDenied);
            }

            var data = await taskManagerUow.FilesBase.GetFile(ids);

            return File(data, file.MimeType);
        }

        public async Task<ActionResult> DownloadFile(string id)
        {
            if (!int.TryParse(id, out int ids))
            {
                return View("_Error", model: Resources.ErrorMessages.AccessDenied);
            }

            var file = await taskManagerUow.Files.FindByIdAsync(ids);

            if (file == null)
            {
                return View("_Error", model: Resources.ErrorMessages.AccessDenied);
            }

            var data = await taskManagerUow.FilesBase.GetFile(ids);

            return File(data, file.MimeType, file.GetNameWithExtension());
        }

    }
}
