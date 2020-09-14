using PDCore.Repositories.Repo;
using PDCoreNew.Services.Serv;
using PDWebCore.Helpers.ExceptionHandling;
using PDWebCore.Helpers.MultiLanguage;
using PDWebCore.Loggers;
using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TaskManager.DAL;
using TaskManager.Web.App_Start;

namespace TaskManager.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ContainerConfig.RegisterContainer(GlobalConfiguration.Configuration);

            log4net.Config.XmlConfigurator.Configure();

            LogService.EnableLogInDb<TaskManagerContext, SqlServerWebLogger>();

            SqlRepository.IsLoggingEnabledByDefault = bool.Parse(WebConfigurationManager.AppSettings["IsLoggingEnabledByDefault"]);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            HttpApplicationErrorHandler.HandleLastException(Server, Request, Response);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            LanguageHelper.SetLanguage(Request);
        }

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{

        //}
    }
}
