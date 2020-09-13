namespace TaskManager.DAL.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PDCore.Extensions;
    using PDCoreNew.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaskManager.DAL.Entities;
    using TaskManager.DAL.SampleData;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaskManagerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            TaskManagerSeeder taskManagerSeeder = new TaskManagerSeeder(context);

            taskManagerSeeder.Seed();
        }
    }
}
