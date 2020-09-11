using PDWebCore.Context;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL
{
    public class TaskManagerContext : MainWebDbContext<ApplicationUser>
    {
        public TaskManagerContext() : base("TaskManagerContext")
        {

        }

        public static TaskManagerContext Create()
        {
            return new TaskManagerContext();
        }
    }
}
