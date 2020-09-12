using PDWebCore.Helpers.MultiLanguage;
using System.Web.Mvc;

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
