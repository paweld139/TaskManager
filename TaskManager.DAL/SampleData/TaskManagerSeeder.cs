using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PDCore.Extensions;
using PDCoreNew.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using TaskManager.BLL.Entities;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.SampleData
{
    public class TaskManagerSeeder
    {
        private readonly TaskManagerContext context;

        public TaskManagerSeeder(TaskManagerContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            SeedRoles(context);
            SeedContrahents(context);
            SeedUsers(context);
            SeedDictionary(context);
            SeedTickets(context);
        }

        private void SeedTickets(TaskManagerContext context)
        {
            string userId = context.Users.First().Id;

            var tickets = new[]
            {
                new Ticket
                {
                    Subject = "Coś nie działa",
                    Description = "Jest bardzo źle",
                    Number = "TIC/2020/09/001",
                    TypeId = 12,
                    PriorityId = 3,
                    StatusId = 6,
                    ContrahentId = 1,
                    RepresentativeId = userId,
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                           Content = "Dzięki",
                           EmployeeId = userId,
                           TicketId = 1
                        },
                        new Comment
                        {
                           Content = "Jeszcze raz dzięki",
                           EmployeeId = userId,
                           TicketId = 1
                        }
                    }
                },

                new Ticket
                {
                    Subject = "Znowu nie działa",
                    Description = "Jest jeszcze gorzej",
                    Number = "TIC/2020/09/002",
                    TypeId = 13,
                    PriorityId = 5,
                    StatusId = 7,
                    ContrahentId = 2,
                    RepresentativeId = userId,
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                           Content = "Witam",
                           EmployeeId = userId,
                           TicketId = 2
                        }
                    }
                }

            };

            tickets.ForEach(x => context.Set<Ticket>().AddOrUpdate(c => c.Number, x));

            context.SaveChangesWithModificationHistory();
        }

        private void SeedDictionary(TaskManagerContext context)
        {
            var vals = new[]
            {
                "Nieokreślony",
                "Nieznany",
                "Niski",
                "Średni",
                "Wysoki"
            };

            AddDictionary("Priorytet", vals, context);


            vals = new[]
            {
                "Przyjęte",
                //"Do zatwierdzenia",
                //"Zatwierdzone",
                //"Do wypuszczenia",
                "Do odbioru",
                "Do wyjaśnienia",
                "Anulowane",
                "Odrzucone",
                "Zakończone"
            };

            AddDictionary("Status", vals, context);


            vals = new[]
            {
                "Serwis",
                "Z wyceny",
                "Handlowe",
                "Wewnętrzne"
            };

            AddDictionary("Typ", vals, context);


            context.SaveChangesWithModificationHistory();
        }

        private void AddDictionary(string name, IEnumerable<string> positions, TaskManagerContext context)
        {
            var dics = positions.Select(x => new Dictionary { Name = name, Value = x });

            dics.ForEach(x => context.Set<Dictionary>().AddOrUpdate(d => new { d.Name, d.Value }, x));
        }

        private void SeedRoles(TaskManagerContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            CreateRole(roleManager, "Serwisant");

            CreateRole(roleManager, "Admin");

            CreateRole(roleManager, "Zatwierdzający");

            CreateRole(roleManager, "Potwierdzający");

            CreateRole(roleManager, "Klient");

            CreateRole(roleManager, "Wewnętrzny");

            roleManager.Dispose();
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
                var applicationUser = new ApplicationUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true,
                    Hometown = "Nowa Ruda",
                };

                var userResult = manager.Create(applicationUser, "hasloos@1Z");

                if (userResult.Succeeded)
                {
                    manager.AddToRole(applicationUser.Id, "Klient");
                }

                var employee = new Employee
                {
                    ContrahentId = 1,
                    FirstName = "Paweł",
                    LastName = "Dywan",
                    Id = applicationUser.Id
                };

                context.Employees.Add(employee);

                context.SaveChangesWithModificationHistory();
            }

            manager.Dispose();

            store.Dispose();
        }

        private void SeedContrahents(TaskManagerContext context)
        {
            var contrahents = new[]
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

            contrahents.ForEach(x => context.Set<Contrahent>().AddOrUpdate(c => c.NIP, x));

            context.SaveChangesWithModificationHistory();
        }
    }
}
