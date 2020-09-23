using System;
using System.Web.Http;
using System.Web.Mvc;
using TaskManager.Web.Areas.HelpPage.ModelDescriptions;
using TaskManager.Web.Areas.HelpPage.Models;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;

namespace TaskManager.Web.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HelpController() : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();

            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (!string.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);

                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (!string.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();

                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out ModelDescription modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}