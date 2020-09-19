using System.Data.Entity;

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
