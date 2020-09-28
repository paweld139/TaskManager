using PDWebCore.Context;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TaskManager.DAL.Configuration;
using TaskManager.DAL.Entities;
using TaskManager.DAL.SampleData;

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

        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            modelBuilder.Configurations.Add(new TicketConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            //modelBuilder.Configurations.Add(new EmployeeConfiguration());
            //modelBuilder.Configurations.Add(new ApplicationUserConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
