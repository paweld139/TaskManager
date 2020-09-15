using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PDCore.Extensions;
using PDCoreNew.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.SampleData
{
    public class TaskManagerDbInitializer :
        //CreateDatabaseIfNotExists<TaskManagerContext>      // when model is stable
        //
        DropCreateDatabaseIfModelChanges<TaskManagerContext> // when iterating
    {
        protected override void Seed(TaskManagerContext context)
        {
            TaskManagerSeeder taskManagerSeeder = new TaskManagerSeeder(context);

            taskManagerSeeder.Seed();


            base.Seed(context);
        }
    }
}
