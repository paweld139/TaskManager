using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Web.Controllers
{
    public class ErrorsController : Controller
    {
        //
        // GET: /Error/
        public ViewResult Index(string message)
        {
            return View("_Error", model: message);
        }
    }
}