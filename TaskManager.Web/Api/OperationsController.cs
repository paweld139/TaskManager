using PDCore.Models;
using PDCore.Services.IServ;
using PDWebCore;
using PDWebCore.Helpers.MultiLanguage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TaskManager.Web.Api
{
    [Authorize]
    [RoutePrefix("api")]
    public class OperationsController : ApiController
    {
        private readonly IMailServiceAsyncTask mailService;

        public OperationsController(IMailServiceAsyncTask mailService)
        {
            this.mailService = mailService;
        }

        // OPTIONS: api/refreshconfig
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpOptions]
        [Route("sendTestEmail")]
        public async Task<IHttpActionResult> SendTestEmail()
        {
            var mailMessage = new MailMessageModel("p.dywan97@gmail.com", "Test", "<strong style=\"color: red\">Zawartość<strong>");

            await mailService.SendEmailAsyncTask(mailMessage);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpOptions]
        [Route("encryptConfig")]
        public IHttpActionResult EncryptConfig()
        {
            bool result = Utils.WebEncrypt() && Utils.WebEncrypt("system.net/mailSettings/smtp");

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpOptions]
        [Route("decryptConfig")]
        public IHttpActionResult DecryptConfig()
        {
            bool result = Utils.WebDecrypt() && Utils.WebDecrypt("system.net/mailSettings/smtp");

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpOptions]
        [Route("toggleEncryptConfig")]
        public IHttpActionResult ToggleEncryptConfig()
        {
            bool result = Utils.ToggleWebEncrypt() & Utils.ToggleWebEncrypt("system.net/mailSettings/smtp");

            return Ok(result);
        }
    }
}
