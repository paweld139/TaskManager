using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using PDWebCore.Helpers.MultiLanguage;

[assembly: OwinStartup(typeof(TaskManager.Web.Startup))]

namespace TaskManager.Web
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            //ControllerBuilder.Current.SetControllerFactory(new DefaultControllerFactory(new MultiLanguageControllerActivator()));

            ConfigureAuth(app);

            DataProtectionProvider = app.GetDataProtectionProvider();
        }
    }
}
