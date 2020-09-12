namespace TaskManager.DAL.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PDCore.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaskManager.BLL.Models;
    using TaskManager.DAL.Entities;

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

            SeedRoles(context);
            SeedContrahents(context);
            SeedUsers(context);
            SeedDictionary(context);
        }

        private void SeedDictionary(TaskManagerContext context)
        {
            List<string> vals = new List<string>
            {
                "Nieokreœlony",
                "Nieznany",
                "Niski",
                "Średni",
                "Wysoki"
            };

            AddDictionary("Priorytet", vals, context);


            vals = new List<string>
            {
                "Przyjęte",
                "Do zatwierdzenia",
                "Zatwierdzone",
                "Do wypuszczenia",
                "Do odbioru",
                "Do wyjaśnienia",
                "Anulowane",
                "Odrzucone",
                "Zakończone"
            };

            AddDictionary("Status", vals, context);


            vals = new List<string>
            {
                "Serwis",
                "Z wyceny",
                "Handlowe",
                "Wewnętrzne"
            };

            AddDictionary("Typ", vals, context);


            context.SaveChanges();
        }

        private void AddDictionary(string name, List<string> positions, TaskManagerContext context)
        {
            var dics = positions.Select(x => new Dictionary { Name = name, Value = x });

            dics.ForEach(x => context.Set<Dictionary>().AddOrUpdate(x));
        }

        private void SeedRoles(TaskManagerContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            CreateRole(roleManager, "Serwisant");

            CreateRole(roleManager, "Admin");

            CreateRole(roleManager, "Zatwierdzaj¹cy");

            CreateRole(roleManager, "Potwierdzaj¹cy");

            CreateRole(roleManager, "Klient");

            CreateRole(roleManager, "Wewnętrzny");
        }

        private void CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!roleManager.RoleExists(roleName))
            {
                var role = new IdentityRole
                {
                    Name = roleName
                };

                roleManager.Create(role);
            }
        }

        private void SeedUsers(TaskManagerContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            string userName = "pawell139139@gmail.com";

            if (!context.Users.Any(u => u.UserName == userName))
            {
                var user = new ApplicationUser
                {
                    UserName = userName,
                    Email = userName,
                    ContrahentId = 1,
                    FirstName = "Pawe³",
                    LastName = "Dywan",
                    CreateDate = DateTime.Now,
                    EmailConfirmed = true
                    //IsBlocked = false,
                    //IsDeleted = false,
                    //IsVerified = false,
                    //CreateDate = DateTime.Now,
                    //BlockDate = null,
                    //DeleteDate = null
                };

                var userResult = manager.Create(user, "hasloos@1Z");

                if (userResult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Serwisant");
                }
            }
        }

        private void SeedContrahents(TaskManagerContext context)
        {
            List<Contrahent> contrahents = new List<Contrahent>()
            {
                new Contrahent()
                {
                    Name = "IBM",
                    NIP = "1234567890",
                    Email = "IBM@wp.pl",
                    IsOperator = true,
                    LicenseKey = "12345678901234567890"
                },

                new Contrahent()
                {
                    Name = "Microsoft",
                    NIP = "1234567891",
                    Email = "Microsoft@wp.pl",
                    LicenseKey = "12345678911234567891"
                }
            };

            contrahents.ForEach(x => context.Set<Contrahent>().AddOrUpdate(x));

            context.SaveChanges();
        }
    }
}
