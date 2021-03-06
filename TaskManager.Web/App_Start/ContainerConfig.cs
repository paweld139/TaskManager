﻿using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using CommonServiceLocator;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PDCore.Commands;
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
using PDCoreNew.Models;
using PDCoreNew.Repositories.Repo;
using PDCoreNew.Services.Serv;
using PDWebCore.Context.IContext;
using PDWebCore.Factories.Fac;
using PDWebCore.Factories.IFac;
using PDWebCore.Models;
using PDWebCore.Services.IServ;
using PDWebCore.Services.Serv;
using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TaskManager.BLL.Factories;
using TaskManager.BLL.Processors;
using TaskManager.BLL.Translators;
using TaskManager.DAL;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;
using TaskManager.DAL.Repositories;
using TaskManager.DAL.Services;
using TaskManager.DAL.Strategies;
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
                   .UsingConstructor(typeof(DbContext))
                   .InstancePerRequest();

            builder.RegisterType<HttpContextPrinciple>()
                   .As<IPrincipal>()
                   .SingleInstance();


            builder.RegisterType<TaskManagerContext>()
                   .As<IEntityFrameworkDbContext>()
                   .As<IMainDbContext>() //LogRepository
                   .As<IMainWebDbContext>() //FileRepository
                   .As<DbContext>();
                   //.InstancePerRequest(); //UserStore

            builder.RegisterType<RepositoryFactories>()
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterType<RepositoryProvider>()
                   .As<IRepositoryProvider>()
                   .InstancePerRequest();

            builder.RegisterType<TaskManagerUow>()
                   .As<ITaskManagerUow>()
                   .InstancePerRequest();

            builder.RegisterType<TicketDataAccessStrategy>()
                   .As<IDataAccessStrategy<Ticket>>()
                   .SingleInstance();

            builder.RegisterType<CommentDataAccessStrategy>()
                   .As<IDataAccessStrategy<Comment>>()
                   .SingleInstance();

            builder.RegisterType<ContrahentDataAccessStrategy>()
                  .As<IDataAccessStrategy<Contrahent>>()
                  .SingleInstance();

            builder.RegisterType<EmployeeDataAccessStrategy>()
                   .As<IDataAccessStrategy<Employee>>()
                   .SingleInstance();

            builder.RegisterType<ApplicationUserDataAccessStrategy>()
                    .As<IDataAccessStrategy<ApplicationUser>>()
                    .InstancePerRequest();

            builder.RegisterType<FileDataAccessStrategy>()
                    .As<IDataAccessStrategy<File>>()
                    .SingleInstance();


            builder.RegisterGeneric(typeof(SqlRepositoryEntityFrameworkDisconnected<>))
                   .As(typeof(ISqlRepositoryEntityFrameworkDisconnected<>))
                   .InstancePerRequest();

            //builder.RegisterType<LogRepo>()
            //       .As<ILogRepo>()
            //       .InstancePerRequest();

            //builder.RegisterType<DictionaryRepository>()
            //       .As<IDictionaryRepository>()
            //       .InstancePerRequest();

            //builder.RegisterType<TicketRepository>()
            //       .As<ITicketRepository>()
            //       .InstancePerRequest();

            builder.RegisterType<ContrahentRepository>()
                   .As<IContrahentRepository>()
                   .InstancePerRequest();


            builder.RegisterType<LogMessageFactory>()
                   .As<ILogMessageFactory>()
                   .SingleInstance();

            builder.RegisterType<UserDataFactory>()
                   .As<IUserDataFactory>()
                   .SingleInstance();


            builder.RegisterType<TraceLogger>()
                   .As<ILogger>()
                   .SingleInstance();


            builder.RegisterType<CacheService>()
                   .As<ICacheService>()
                   .SingleInstance();

            builder.RegisterType<UserDataService>()
                   .As<IUserDataService>()
                   .InstancePerRequest();

            builder.RegisterType<TicketService>()
                 .AsSelf()
                 .InstancePerRequest();

            builder.RegisterType<TimeService>()
                   .As<ITimeService>();

            builder.RegisterType<MailServiceAsyncTask>()
                   .As<IMailServiceAsyncTask>()
                   .UsingConstructor(typeof(ILogger))
                   .SingleInstance();

            builder.RegisterType<TaskManagerTranslator>()
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterType<SetStatusCommandFactory>()
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterType<CommandManager>()
                   .AsSelf()
                   .InstancePerRequest();

            builder.RegisterType<StatusProcessor>()
                   .AsSelf()
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
