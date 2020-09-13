using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TaskManager.Web.Api
{
    public class OperationsController : ApiController
    {
        [HttpOptions]
        [Route("api/refreshconfig")]
        public IHttpActionResult RefreshAppSettings()
        {
            ConfigurationManager.RefreshSection("AppSettings");

            return Ok();
        }
    }
}
