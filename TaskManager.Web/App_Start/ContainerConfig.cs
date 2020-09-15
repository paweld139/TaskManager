using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using CommonServiceLocator;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PDCore.Factories.Fac;
using PDCore.Factories.IFac;
using PDCore.Interfaces;
using PDCore.Repositories.IRepo;
using PDCore.Services.IServ;
using PDCore.Services.Serv.Time;
using PDCoreNew.Context.IContext;
using PDCoreNew.Factories.Fac.Repository;
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
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TaskManager.DAL;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Repositories;
using CacheService = PDWebCore.Services.Serv.CacheService;

namespace TaskManager.Web.App_Start
{
    public class ContainerConfig
    {
        private static void RegisterServices(ContainerBuilder builder)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TaskManagerMappingProfile());
            });

            builder.RegisterInstance(mapperConfiguration.CreateMapper())
                   .As<IMapper>()
                   .SingleInstance();


            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication)
                   .InstancePerRequest();

            builder.RegisterType<ApplicationUserManager>()
                   .InstancePerRequest();

            builder.RegisterType<ApplicationSignInManager>()
                   .InstancePerRequest();

            builder.RegisterType<UserStore<ApplicationUser>>()
                   .As<IUserStore<ApplicationUser>>()
                   .UsingConstructor(typeof(DbContext));


            builder.RegisterType<TaskManagerContext>()
                   .As<IEntityFrameworkDbContext>()
                   .As<IMainDbContext>() //LogRepository
                   .As<IMainWebDbContext>() //FileRepository
                   .As<DbContext>(); //UserStore

            builder.RegisterType<RepositoryFactories>()
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterType<RepositoryProvider>()
                   .As<IRepositoryProvider>()
                   .InstancePerRequest();

            builder.RegisterType<TaskManagerUow>()
                   .As<ITaskManagerUow>()
                   .InstancePerRequest();


            builder.RegisterGeneric(typeof(SqlRepositoryEntityFrameworkDisconnected<>))
                   .As(typeof(ISqlRepositoryEntityFrameworkDisconnected<>))
                   .InstancePerRequest();

            //builder.RegisterType<LogRepo>()
            //       .As<ILogRepo>()
            //       .InstancePerRequest();

            //builder.RegisterType<DictionaryRepository>()
            //       .As<IDictionaryRepository>()
            //       .InstancePerRequest();


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
        }

        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            RegisterServices(builder);

            //builder.RegisterWebApiFilterProvider(httpConfiguration);
            //builder.RegisterWebApiModelBinderProvider();


            var container = builder.Build();


            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
            

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
