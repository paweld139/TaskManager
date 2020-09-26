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
            SeedRoles();
            SeedContrahents();
            SeedUsers();
            SeedDictionary();
            SeedTickets();
        }

        private void SeedTickets()
        {
            var users = context.Users.ToArray();

            string adminId = users.Single(u => u.UserName.Contains("admin")).Id;
            string operatorId = users.Single(u => u.UserName.Contains("serwisant")).Id;
            string customerId = users.Single(u => u.UserName.Contains("klient")).Id;

            var tickets = new[]
            {
                new Ticket
                {
                    Subject = "Coś nie działa",
                    Description = "Jest źle",
                    Number = "TIC/2020/09/001",
                    TypeId = 12,
                    PriorityId = 1,
                    StatusId = 6,
                    ContrahentId = 2,
                    RepresentativeId = customerId,
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                           Content = "Jestem klientem i dodałem to zadanie",
                           EmployeeId = customerId,
                           TicketId = 1
                        },
                        new Comment
                        {
                           Content = "Jestem serwisantem",
                           EmployeeId = operatorId,
                           TicketId = 1
                        },
                        new Comment
                        {
                           Content = "Adminem jestem",
                           EmployeeId = adminId,
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
                    PriorityId = 2,
                    StatusId = 7,
                    ContrahentId = 1,
                    RepresentativeId = operatorId,
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                           Content = "Jestem serwisantem i dodałem to zadanie. Klient nie widzi tego zadania.",
                           EmployeeId = operatorId,
                           TicketId = 2
                        },
                        new Comment
                        {
                           Content = "Adminem jestem",
                           EmployeeId = adminId,
                           TicketId = 2
                        }
                    }
                },
                new Ticket
                {
                    Subject = "Jest najgorzej",
                    Description = "Gorzej być nie może",
                    Number = "TIC/2020/09/003",
                    TypeId = 14,
                    PriorityId = 3,
                    StatusId = 8,
                    ContrahentId = 1,
                    RepresentativeId = adminId,
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                           Content = "Jestem adminem i dodałem to zadanie. Klient nie widzi tego zadania.",
                           EmployeeId = adminId,
                           TicketId = 3
                        },
                        new Comment
                        {
                           Content = "Serwisantem jestem",
                           EmployeeId = operatorId,
                           TicketId = 3
                        }
                    }
                }
            };

            tickets.ForEach(x => context.Set<Ticket>().AddOrUpdate(c => c.Number, x));

            context.SaveChangesWithModificationHistory();
        }

        private void SeedDictionary()
        {
            var vals = new[]
            {
                "Nieokreślony",
                "Nieznany",
                "Niski",
                "Średni",
                "Wysoki"
            };

            AddDictionary("Priorytet", vals);


            vals = new[]
            {
                "Przyjęte",
                "Do odbioru",
                "Do wyjaśnienia",
                "Anulowane",
                "Odrzucone",
                "Zakończone"
            };

            AddDictionary("Status", vals);


            vals = new[]
            {
                "Serwis",
                "Z wyceny",
                "Handlowe",
                "Wewnętrzne"
            };

            AddDictionary("Typ", vals);


            context.SaveChangesWithModificationHistory();
        }

        private void AddDictionary(string name, IEnumerable<string> positions)
        {
            var dics = positions.Select(x => new Dictionary { Name = name, Value = x });

            dics.ForEach(x => context.Set<Dictionary>().AddOrUpdate(d => new { d.Name, d.Value }, x));
        }

        private void SeedRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            CreateRole(roleManager, "Serwisant");

            CreateRole(roleManager, "Admin");

            CreateRole(roleManager, "Klient");

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

        private void SeedUsers()
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            var employee = new Employee
            {
                ContrahentId = 2,
                FirstName = "Paweł",
                LastName = "Klient",
            };

            CreateUser(manager, "klient@gmail.com", "hasloos@1Z", "Klient", "Klientów", employee);


            employee = new Employee
            {
                ContrahentId = 1,
                FirstName = "Paweł",
                LastName = "Operator",
            };

            CreateUser(manager, "serwisant@gmail.com", "hasloos@1Z2", "Serwisant", "Serwisantów", employee);


            employee = new Employee
            {
                ContrahentId = 1,
                FirstName = "Paweł",
                LastName = "Admin",
            };

            CreateUser(manager, "admin@gmail.com", "hasloos@1Z3", "Admin", "Adminów", employee);


            manager.Dispose();

            store.Dispose();
        }

        private void CreateUser(UserManager<ApplicationUser> manager,
            string userName,
            string password,
            string roleName,
            string hometown,
            Employee employee)
        {
            if (!context.Users.Any(u => u.UserName == userName))
            {
                var applicationUser = new ApplicationUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true,
                    Hometown = hometown
                };

                var userResult = manager.Create(applicationUser, password);

                if (userResult.Succeeded)
                {
                    manager.AddToRole(applicationUser.Id, roleName);


                    employee.Id = applicationUser.Id;

                    context.Employees.Add(employee);


                    context.SaveChangesWithModificationHistory();
                }
            }
        }

        private void SeedContrahents()
        {
            var contrahents = new[]
            {
                new Contrahent()
                {
                    Name = "IBM Serwisant",
                    NIP = "1234567890",
                    Email = "IBM@wp.pl",
                    IsOperator = true,
                    LicenseKey = "12345678901234567890"
                },

                new Contrahent()
                {
                    Name = "Microsoft Klient",
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
