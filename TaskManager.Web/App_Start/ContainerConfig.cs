using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CommonServiceLocator;
using PDCore.Factories.Fac;
using PDCore.Factories.IFac;
using PDCore.Interfaces;
using PDCore.Repositories.IRepo;
using PDCore.Services.IServ;
using PDCore.Services.Serv.Time;
using PDCoreNew.Context.IContext;
using PDCoreNew.Helpers;
using PDCoreNew.Loggers;
using PDCoreNew.Repositories.IRepo;
using PDCoreNew.Repositories.Repo;
using PDCoreNew.Services.Serv;
using PDWebCore.Context.IContext;
using PDWebCore.Factories.Fac;
using PDWebCore.Factories.IFac;
using PDWebCore.Services.IServ;
using PDWebCore.Services.Serv;
using System.Web.Http;
using System.Web.Mvc;
using TaskManager.DAL;
using CacheService = PDWebCore.Services.Serv.CacheService;

namespace TaskManager.Web.App_Start
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();


            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);


            builder.RegisterType<TaskManagerContext>()
                   .As<IEntityFrameworkDbContext>()
                   .As<IMainDbContext>() //LogRepository
                   .As<IMainWebDbContext>(); //FileRepository

            builder.RegisterGeneric(typeof(SqlRepositoryEntityFrameworkDisconnected<>))
                   .As(typeof(ISqlRepositoryEntityFrameworkDisconnected<>))
                   .InstancePerRequest();

            builder.RegisterType<LogRepo>()
                   .As<ILogRepo>()
                   .InstancePerRequest();


            builder.RegisterType<LogMessageFactory>()
                   .As<ILogMessageFactory>()
                   .SingleInstance();

            builder.RegisterType<UserDataFactory>()
                   .As<IUserDataFactory>()
                   .SingleInstance();


            builder.RegisterType<FileLogger>()
                   .As<ILogger>()
                   .SingleInstance();


            builder.RegisterType<CacheService>()
                   .As<ICacheService>()
                   .SingleInstance();

            builder.RegisterType<UserDataService>()
                   .As<IUserDataService>()
                   .InstancePerRequest();

            builder.RegisterType<TimeService>()
                   .As<ITimeService>();

            builder.RegisterType<MailServiceAsyncTask>()
                   .As<IMailServiceAsyncTask>()
                   .UsingConstructor(typeof(ILogger))
                   .SingleInstance();


            var container = builder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
