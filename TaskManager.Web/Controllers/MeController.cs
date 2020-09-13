using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using TaskManager.Web.Models;

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class MeController : ApiController
    {
        private readonly ApplicationUserManager userManager;

        public MeController(ApplicationUserManager userManager)
        {
            this.userManager = userManager;
        }

        // GET api/Me
        public GetViewModel Get()
        {
            var user = userManager.FindById(User.Identity.GetUserId());

            return new GetViewModel() { Hometown = user.Hometown };
        }
    }
}