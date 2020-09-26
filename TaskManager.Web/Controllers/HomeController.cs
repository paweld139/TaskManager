using Microsoft.AspNet.Identity;
using PDCoreNew.Extensions;
using PDWebCore.Helpers.MultiLanguage;
using System.Web.Mvc;

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.isLoggedIn = User.Identity.IsAuthenticated;
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.roles = User.Identity.GetRoles();
            ViewBag.employeeId = User.Identity.GetEmployeeId();
            ViewBag.contrahentId = User.Identity.GetContrahentId();

            return View();
        }

        [AllowAnonymous]
        public ActionResult ChangeLanguage(string lang)
        {
            LanguageHelper.SetLanguage(lang);

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
