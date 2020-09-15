using PDWebCore.Helpers.MultiLanguage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TaskManager.Web.Api
{
    [RoutePrefix("api")]
    public class OperationsController : ApiController
    {
        // OPTIONS: api/refreshconfig
        [HttpOptions]
        [Route("refreshconfig")]
        public IHttpActionResult RefreshAppSettings()
        {
            ConfigurationManager.RefreshSection("AppSettings");

            return Ok();
        }

        [HttpOptions]
        [Route("setLanguage")]
        public IHttpActionResult SetLanguage(string lang)
        {
            LanguageHelper.SetLanguage(lang);

            return Ok();
        }
    }
}
