using PDWebCore.Context;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TaskManager.BLL.Entities;
using TaskManager.DAL.Configuration;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL
{
    [DbConfigurationType(typeof(TaskManagerDbConfiguration))]
    public class TaskManagerContext : MainWebDbContext<ApplicationUser>
    {
        public TaskManagerContext() : base("TaskManagerContext")
        {
#if DEBUG
            Database.SetInitializer(new NullDatabaseInitializer<TaskManagerContext>()); //Na produkcji też
#else     
            Database.SetInitializer(new TaskManagerDbInitializer());
#endif

            //this.ConfigureForDateTimeKind(DateTimeKind.Utc);
        }


        public DbSet<Dictionary> Dictionaries { get; set; }

        public DbSet<Contrahent> Contrahents { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            modelBuilder.Configurations.Add(new TicketConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
